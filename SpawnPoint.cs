using Godot;
using System;
using System.Collections.Generic;

public partial class SpawnPoint : Marker2D
{
    HashSet<BiomeEnum> biomesCache = new HashSet<BiomeEnum>();
    PhysicsPointQueryParameters2D pointParams = new PhysicsPointQueryParameters2D();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        pointParams.CollideWithAreas = true;
        pointParams.CollideWithBodies = false;
        pointParams.CollisionMask = ((1 << 15)); //32768; //bitmask for collision layer 16, yes there was probably a way to auto gen this
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public PackedScene TrySpawn()
    {
        List<Area2D> areas = new List<Area2D>();

        pointParams.Position = GlobalPosition;
        var thingy = GetWorld2D().DirectSpaceState.IntersectPoint(pointParams, 32);
        //areas.AddRange(GetOverlappingAreas());
        //GD.Print(thingy.ToString());

        biomesCache.Clear();
        foreach ( var thing in thingy)
        {
            Variant valeee;
            thing.TryGetValue("collider", out valeee);
            biomesCache.Add(((Area2D)valeee as BiomeBase).biome);
            //Godot dictionaries are cancer
        }
        return SlimeSpawner.GetSlimeToSpawn(biomesCache);
    }
}
