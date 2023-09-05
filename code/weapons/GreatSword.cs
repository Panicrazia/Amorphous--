using Godot;
using System;
using System.Transactions;

public partial class GreatSword : Weapon
{
    PackedScene scene = GD.Load<PackedScene>("res://code/weapons/Slash.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void MakeSlice()
	{
		Node2D instance = (Node2D)scene.Instantiate();
		instance.Scale = new Vector2(.75f, .75f);
		instance.Rotate(Mathf.DegToRad(200f));

        //instance.Rotate((float)(1f *Math.PI));
		AddChild(instance);
    }
}
