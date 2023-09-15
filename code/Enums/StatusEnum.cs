using Godot;
using System;
using static SpawnWeights;

public enum StatusEnum
{
	None,
	StickyGoo,
	Acid,
	Ink
}
static class StatusStuff
{
    public static int GetDefaultDuration(this StatusEnum status)
    {
        switch (status)
        {
            case StatusEnum.StickyGoo:
                return 300;
            case StatusEnum.Acid:
                return 20;
            case StatusEnum.Ink:
                return 90;
            default:
                return 1;
        }
    }
}

