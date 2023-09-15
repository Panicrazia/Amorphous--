using Godot;
using System;

public enum SlimeType
{
    Green,
    Yellow,
    Biter,
    Acid
}
static class SlimeTypeMethods
{
    public static PackedScene GetSmallSlime(this SlimeType slime)
    {
        switch (slime)
        {
            case SlimeType.Green:
                return GD.Load<PackedScene>("res://code/enemies/Enemy.tscn");
            case SlimeType.Yellow:
                return GD.Load<PackedScene>("res://code/enemies/YellowSlime.tscn");
            case SlimeType.Biter:
                return GD.Load<PackedScene>("res://code/enemies/Enemy.tscn");
            case SlimeType.Acid:
                return GD.Load<PackedScene>("res://code/enemies/Enemy.tscn");
            default:
                return null;
        }
    }
    public static PackedScene GetMedSlime(this SlimeType slime)
    {
        switch (slime)
        {
            case SlimeType.Green:
                return GD.Load<PackedScene>("res://code/enemies/BigEnemy.tscn");
            case SlimeType.Yellow:
                return GD.Load<PackedScene>("res://code/enemies/YellowSlime.tscn");
            case SlimeType.Biter:
                return GD.Load<PackedScene>("res://code/enemies/Enemy.tscn");
            case SlimeType.Acid:
                return GD.Load<PackedScene>("res://code/enemies/Enemy.tscn");
            default:
                return null;
        }
    }
}
