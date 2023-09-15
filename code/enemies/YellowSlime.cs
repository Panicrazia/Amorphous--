using Godot;
using System;

public partial class YellowSlime : Enemy
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        maxSpeed = SlimeConstants.YellowSlimeMaxSpeed;
        gibColor = SlimeConstants.YellowSlimeColor;
    }

    public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}

    public override Splatter1 MakeSplatter(Vector2 originPos, float scale)
    {
        Splatter1 splatter = base.MakeSplatter(originPos, .67f);
        splatter.SetSticky(300f); //TODO: magic number
        splatter.SetStatusToInflict(StatusEnum.StickyGoo);
        return splatter;
    }
}
