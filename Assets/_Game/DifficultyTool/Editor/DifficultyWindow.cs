using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DifficultyWindow : EditorWindow
{
    private DifficultyWindowData data;
    private DifficultyApplier applier;
    private int appliedPresetIndex;

    private SerializedObject so;
    private SerializedProperty propPresets;
    private SerializedProperty propScalars;

    [MenuItem("Tools/Difficulty")]
    public static void CreateWindow() => GetWindow<DifficultyWindow>("Difficulty");

    private void OnEnable()
    {
        if (data == null)
        {
            data = AssetDatabase.LoadAssetAtPath<DifficultyWindowData>("Assets/_Game/DifficultyTool/DifficultyWindowData.asset");
            if(data != null)
            {
                InitSo();
                return;
            }

            data = CreateInstance<DifficultyWindowData>();
            AssetDatabase.CreateAsset(data, "Assets/_Game/DifficultyTool/DifficultyWindowData.asset");
            AssetDatabase.Refresh();
        }

        InitSo();
    }

    private void InitSo()
    {
        so = new SerializedObject(data);
        propPresets = so.FindProperty("presets");
        propScalars = so.FindProperty("difficultyScalars");
        appliedPresetIndex = data.defaultPresetIndex;
    }

    private void OnGUI()
    {
        so.Update();
        
        EditorGUILayout.PropertyField(propPresets);
        DrawDifficultySelectors();
        DrawScalars();
        DrawDefaultDifficulty();

        if (so.ApplyModifiedProperties())
        {
            if(data.selectedPreset != null)
                EditorUtility.SetDirty(data.selectedPreset);
        }
    }

    private void DrawDifficultySelectors()
    {
        if(data.presets == null) return;
        using (new EditorGUILayout.HorizontalScope())
        {
            foreach (var preset in data.presets)
            {
                if (preset == null) continue;
                if (GUILayout.Button(preset.presetName))
                {
                    SetPreset(preset);
                }
            }
        }
    }

    private void SetPreset(DifficultyPreset preset)
    {
        data.selectedPreset = preset;
        data.difficultyScalars = preset.scalars;
    }

    private void DrawScalars()
    {
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("health"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("damageOfAsteroids"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("destroyProjectileOnTouch"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("spawnRateRange"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("spawnAmountRange"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("asteroidSizeRange"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("asteroidSpeedRange"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("asteroidRotationSpeedRange"));
        EditorGUILayout.Space();
    }

    private void DrawDefaultDifficulty()
    {
        GetApplier();

        string[] names = new string[data.presets.Count];
        for (int i = 0; i < data.presets.Count; i++) 
            names[i] = data.presets[i].presetName;
        
        EditorGUILayout.LabelField($"Current default difficulty preset : {data.presets[appliedPresetIndex].presetName}");
        data.defaultPresetIndex = EditorGUILayout.Popup(data.defaultPresetIndex, names);
        if (GUILayout.Button("Set as default"))
        {
            Undo.RecordObject(applier, "Set Difficulty");
            applier.defaultPreset = data.presets[data.defaultPresetIndex];
            appliedPresetIndex = data.defaultPresetIndex;
            EditorUtility.SetDirty(applier);
        }
    }

    private void GetApplier()
    {
        if (applier == null)
        {
            applier = FindObjectOfType<DifficultyApplier>();
            if (applier == null)
            {
                if (GUILayout.Button("Add DifficultyApplier to scene"))
                {
                    GameObject da = new GameObject("Difficulty Applier");
                    applier = da.AddComponent<DifficultyApplier>();
                }
            }
        }
    }
}
