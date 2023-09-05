using Godot;
using System;
using System.Collections.Generic;

public static class GibManager
{
	static List<PackedScene> Splatters = new List<PackedScene> {
        GD.Load<PackedScene>("res://code/gibsSlimeAndCorpses/splatter1.tscn"),
        GD.Load<PackedScene>("res://code/gibsSlimeAndCorpses/splatter2.tscn"),
        GD.Load<PackedScene>("res://code/gibsSlimeAndCorpses/splatter3.tscn")
    };

    //Engine.RegisterSingleton("GibManager", this);


	public static Splatter1 getSplatter(Vector2 pos, float rot, Vector2 scal)
    {
        Splatter1 splatter = Splatters[GD.RandRange(0, Splatters.Count - 1)].Instantiate() as Splatter1;
        splatter.SetPosRotScale(pos, rot, scal);
        return splatter;
    }
}
