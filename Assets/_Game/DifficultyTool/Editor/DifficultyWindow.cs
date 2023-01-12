using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DifficultyWindow : EditorWindow
{
    public List<DifficultyPreset> presets;
    public DifficultyScalars difficultyScalars;

    private SerializedObject so;
    private SerializedProperty propPresets;
    private SerializedProperty propScalars;

    [MenuItem("Tools/Difficulty")]
    public static void CreateWindow() => GetWindow<DifficultyWindow>("Difficulty");

    private void OnEnable()
    {
        so = new SerializedObject(this);
        propPresets = so.FindProperty("presets");
        propScalars = so.FindProperty("difficultyScalars");
    }

    private void OnGUI()
    {
        so.Update();
        
        EditorGUILayout.PropertyField(propPresets);
        DrawDifficultySelectors();
        DrawScalars();
        

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
                {
                    difficultyScalars = preset.scalars;
                }
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
}
