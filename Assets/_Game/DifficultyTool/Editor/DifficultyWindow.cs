using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DifficultyWindow : EditorWindow
{
    public List<DifficultyPreset> presets;
    public DifficultyScalars difficultyScalars;
    public DifficultyApplier applier;
    public int defaultPresetIndex;

    private SerializedObject so;
    private SerializedProperty propPresets;
    private SerializedProperty propScalars;
    private SerializedProperty propApplier;
    private SerializedProperty propDefaultPresetIndex;

    [MenuItem("Tools/Difficulty")]
    public static void CreateWindow() => GetWindow<DifficultyWindow>("Difficulty");

    private void OnEnable()
    {
        so = new SerializedObject(this);
        propPresets = so.FindProperty("presets");
        propScalars = so.FindProperty("difficultyScalars");
        propApplier = so.FindProperty("applier");
        propDefaultPresetIndex = so.FindProperty("defaultPresetIndex");
    }

    private void OnGUI()
    {
        so.Update();
        
        EditorGUILayout.PropertyField(propPresets);
        DrawDifficultySelectors();
        DrawScalars();
        DrawDefaultDifficulty();

        so.ApplyModifiedProperties();
    }

    private void DrawDifficultySelectors()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            foreach (var preset in presets)
            {
                if (preset == null) continue;
                if (GUILayout.Button(preset.presetName)) 
                    difficultyScalars = preset.scalars;
            }
        }
    }

    private void DrawScalars()
    {
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("health"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("damageOfAsteroids"));
        EditorGUILayout.PropertyField(propScalars.FindPropertyRelative("projectileHealth"));
        EditorGUILayout.Space();
    }

    private void DrawDefaultDifficulty()
    {
        EditorGUILayout.PropertyField(propApplier);
        if(applier == null) return;

        string[] names = new string[presets.Count];
        for (int i = 0; i < presets.Count; i++) names[i] = presets[i].presetName;
        defaultPresetIndex = EditorGUILayout.Popup(defaultPresetIndex, names);
    }
}
