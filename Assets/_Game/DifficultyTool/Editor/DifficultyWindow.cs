using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DifficultyWindow : EditorWindow
{
    private DifficultyWindowData windowData;
    private DifficultyApplier applier;
    private int appliedPresetIndex;

    private SerializedObject so;
    private SerializedProperty propPresets;
    private SerializedProperty propScalars;

    [MenuItem("Tools/Difficulty")]
    public static void CreateWindow() => GetWindow<DifficultyWindow>("Difficulty");

    private void OnEnable()
    {
        if (windowData == null)
        {
            windowData = AssetDatabase.LoadAssetAtPath<DifficultyWindowData>("Assets/_Game/DifficultyTool/DifficultyWindowData.asset");
            if(windowData != null)
            {
                InitSo();
                return;
            }

            windowData = CreateInstance<DifficultyWindowData>();
            AssetDatabase.CreateAsset(windowData, "Assets/_Game/DifficultyTool/DifficultyWindowData.asset");
            AssetDatabase.Refresh();
        }

        InitSo();
    }

    private void InitSo()
    {
        so = new SerializedObject(windowData);
        propPresets = so.FindProperty("presets");
        propScalars = so.FindProperty("difficultyScalars");
        appliedPresetIndex = windowData.defaultPresetIndex;
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
            if(windowData.selectedPreset != null)
                EditorUtility.SetDirty(windowData.selectedPreset);
        }
    }

    private void DrawDifficultySelectors()
    {
        EditorGUILayout.Space();
        if(windowData.presets == null) return;
        using (new EditorGUILayout.HorizontalScope())
        {
            foreach (var preset in windowData.presets)
            {
                if (preset == null) continue;
                GUIStyle style = new(GUI.skin.GetStyle("Button"));
                if (windowData.selectedPreset == preset) 
                    style.fontStyle = FontStyle.BoldAndItalic;

                if (GUILayout.Button(preset.presetName, style)) 
                    SetPreset(preset);
            }
        }
    }

    private void SetPreset(DifficultyPreset preset)
    {
        windowData.selectedPreset = preset;
        windowData.difficultyScalars = preset.scalars;
    }

    private void DrawScalars()
    {
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("health"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("damageOfAsteroids"));
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

        string[] names = new string[windowData.presets.Count];
        for (int i = 0; i < windowData.presets.Count; i++) 
            names[i] = windowData.presets[i].presetName;
        
        EditorGUILayout.LabelField($"Current default difficulty preset : {windowData.presets[appliedPresetIndex].presetName}");
        windowData.defaultPresetIndex = EditorGUILayout.Popup(windowData.defaultPresetIndex, names);
        if (GUILayout.Button("Set as default"))
        {
            Undo.RecordObject(applier, "Set Difficulty");
            applier.defaultPreset = windowData.presets[windowData.defaultPresetIndex];
            appliedPresetIndex = windowData.defaultPresetIndex;
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
