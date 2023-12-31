using Godot;
using System;

public partial class Slash : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Eliminate()
	{
		this.QueueFree();
	}

	public void Hit(Node2D thing)
	{
		if (thing is SlimeCore)
		{
			(thing as SlimeCore).TakeDamage(10f, GlobalPosition);
		}
	}
}
