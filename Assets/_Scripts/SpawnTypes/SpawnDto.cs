using System;

[Serializable]
public class SpawnDto
{
    public float Duration { get; set; } = float.MaxValue;
    public float Rotation { get; set; }
    public int Degrees { get; set; }
    public float ShrinkSpeed { get; set; }
    public RotationTypes RotationType { get; set; }
    public float RotationSpeed { get; set; }
    public bool CanBeOverridden { get; set; } = true;
}

public enum RotationTypes { None, Left, Right, Random };