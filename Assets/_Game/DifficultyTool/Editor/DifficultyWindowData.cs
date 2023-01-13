using System.Collections.Generic;
using UnityEngine;

public class DifficultyWindowData : ScriptableObject
{
    public List<DifficultyPreset> presets;
    public DifficultyScalars difficultyScalars;
    public int defaultPresetIndex;
    public DifficultyPreset selectedPreset;
}