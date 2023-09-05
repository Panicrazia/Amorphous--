using Godot;
using System;

public partial class Weapon : Node2D
{
    private AnimationPlayer animPlayer;

    [Signal]
    public delegate void AttackOverSignalEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    public void Attack()
    {
        animPlayer.Play("slash");
    }

    public void AttackOver()
    {
        animPlayer.Stop();
        EmitSignal(SignalName.AttackOverSignal);
    }

}
