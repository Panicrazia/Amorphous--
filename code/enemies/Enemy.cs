using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    public float health = 1f;
    public bool isMerging = false;
    public Enemy merger = null;
    public Node2D target = null;
    public Vector2 targetPos = Vector2.Inf;
    float maxSpeed = Constants.greenSlimeMaxSpeed;
    PackedScene splatterScene = GD.Load<PackedScene>("res://code/gibsSlimeAndCorpses/splatter1.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        health = 1f;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        if (isMerging)
        {
            if (merger != null)
            {
                
                Vector2 distance = (merger.Position - this.Position);
                targetPos = (distance * .5f) + Position;
                if (distance.LengthSquared() < Math.Pow((GetNode<CollisionShape2D>("SlimeHitbox").Shape as CircleShape2D).Radius * .55, 2))
                {
                    //replace this with a spawning manager that handles all spawning
                    Enemy newBoy = ResourceLoader.Load<PackedScene>("res://code/enemies/BigEnemy.tscn").Instantiate() as Enemy;
                    newBoy.Position = targetPos;
                    //newBoy.Velocity = new Vector2(10, 0).Rotated(0);
                    //GD.Print("apwned");
                    AddSibling(newBoy);
                    QueueFree();
                    merger.QueueFree();
                    merger.ResetMergerInfo();
                }
                targetPos = (distance * .5f) + Position;
                MoveAndCollide(((targetPos - Position).Normalized() * 10f).LimitLength(distance.Length()) * (float)delta, false, .50f, true);
                //.Clamp((targetPos - Position), (targetPos - Position))
            }
            else
            {
                ResetMergerInfo();
            }
        }
        else
        {
            KinematicCollision2D collisionInfo = MoveAndCollide(Velocity * (float)delta, false, .50f, true);
            if (collisionInfo != null)
            {
                if ((GD.Randi() % 5) == 0)//25 TODO
                {
                    if (collisionInfo.GetCollider().GetType() == this.GetType())
                    {
                        RecieveMergerInfo(true, collisionInfo.GetCollider() as Enemy);
                        merger.RecieveMergerInfo(true, this);
                    }
                }
                else
                {
                    Velocity = Velocity.Bounce(collisionInfo.GetNormal())*.1f;
                }
                //Velocity = Vector2.Zero;
            }

            if (Velocity.Length() < maxSpeed)
            {
                if (Velocity.Length() < 10f)
                {
                    Velocity = (Velocity * 10f).LimitLength(10f);
                }
                Velocity = (Velocity * 1.04f);
            }
            else if(Velocity.Length() > maxSpeed)
            {
                Velocity = Velocity.LimitLength(maxSpeed);
            }
        }
        
        /**/
    }

    public void RecieveMergerInfo(bool mergerStatus, Enemy thingToMergeWith)
    {
        isMerging = mergerStatus;
        merger = thingToMergeWith;
        this.SetCollisionMaskValue(1, false);
        this.SetCollisionLayerValue(1, false);
    }

    public void ResetMergerInfo()
    {
        isMerging = false;
        merger = null;
        Velocity = (Velocity.Normalized() * 10f).LimitLength(10f);
    }
	
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
        //GD.Print("yeeted and deleted");
        if (merger!=null)
        {
            merger.ResetMergerInfo();
        }
        QueueFree();
	}

    private void _on_body_entered(Node2D body)
    {
        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("SlimeHitbox").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        //GetNode<CollisionShape2D>("CenterHitbox").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }

    public virtual void TakeDamage(float damage, Vector2 originPos)
    {
        health -= damage;
        if(health <= 0)
        {
            Kill(originPos);
        }
    }

    public virtual void Kill(Vector2 originPos)
    {
        QueueFree();
        Splatter1 splatter = GibManager.getSplatter(this.Position, originPos.AngleToPoint(Position), Vector2.One * .67f);
        AddSibling(splatter);
    }

    private void _on_mouse_entered()
    {/*
        GD.Print("=====");
        GD.Print(this.Velocity);
        GD.Print(this.Velocity.Length());
    */
    }
}