using Godot;
using System;
using System.Transactions;

public partial class BigEnemy : Enemy
{
    //do a spawning animation where its getting bigger
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        health = 25f;
        target = Engine.GetSingleton("Player1") as Player;
        Rotation = Position.AngleToPoint(target.Position);
        //LookAt(targetPos); //uhh just deletes the enemy for some reason???? it should be identical to the above line but ok
        Velocity = new Vector2(20, 0).Rotated(Position.AngleToPoint(target.Position));
        Scale = new Vector2(.4f, .4f);
    }

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
        if(target != null)
        {
            //LookAt(target.Position);
            //Rotate( Position.AngleToPoint(target.Position) * .05f);
            //Position.AngleToPoint(target.Position)
            Rotation = (float)Mathf.LerpAngle(Rotation, Position.AngleToPoint(target.Position), delta*.7);
            //GD.Print(Position.AngleToPoint(target.Position));
            Velocity = Vector2.FromAngle(Rotation) * Velocity.Length();
        }
        if(Scale.X < 1f)
        {
            Scale = (Scale * 1.015f).Clamp(Vector2.Zero, new Vector2(1,1));
        }
        if (Velocity.Length() < 130f)
        {
            Velocity = (Velocity * 1.1f);
        }
        else if (Velocity.Length() > 130f)
        {
            Velocity.LimitLength(130f);
        }
        MoveAndSlide();
        /*
        Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();*/
    }

    public override void TakeDamage(float damage, Vector2 originPos)
    {
        base.TakeDamage(damage, originPos);
        Splatter1 splatter = GibManager.getSplatter(this.Position, originPos.AngleToPoint(Position), Vector2.One * .67f);
        AddSibling(splatter);
    }


    public override void Kill(Vector2 originPos)
    {
        base.Kill(originPos);
        QueueFree();
        Splatter1 splatter = GibManager.getSplatter(this.Position, originPos.AngleToPoint(Position), Vector2.One * 1.5f);
        AddSibling(splatter);
    }
}
