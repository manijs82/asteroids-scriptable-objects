using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyPreset", menuName = "DifficultyTool/DifficultyPreset")]
public class DifficultyPreset : ScriptableObject
{
    public string presetName;
    public DifficultyScalars scalars;
}
