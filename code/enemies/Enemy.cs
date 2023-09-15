using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : CharacterBody2D//, IStatusAffected
{
    public float health = 1f;
    public bool isMerging = false;
    public Enemy merger = null;
    public Node2D target = null;
    public Vector2 targetPos = Vector2.Inf;
    public float maxSpeed;
    /*
    public float maxSpeed { 
        get {
            if (statuses.ContainsKey(StatusEnum.StickyGoo))
            {
                return maxSpeed * .4f;
            }
            else
            {
                return maxSpeed;
            }
        } set => maxSpeed = value; }
    */
    public Color gibColor = SlimeConstants.GreenSlimeColor;

    public Dictionary<StatusEnum, int> statuses { get => statuses;  set => statuses = value; }

    //public HashSet<StatusEnum> statuses => statuses;

    //PackedScene splatterScene = GD.Load<PackedScene>("res://code/gibsSlimeAndCorpses/splatter1.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //statuses = new Dictionary<StatusEnum, int>();
        maxSpeed = SlimeConstants.GreenSlimeMaxSpeed;
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

        //probably change this so that things can still bounce and pop them, but they cannot merge with them
        //(also gives possibilities for a special merger where one joins in two already merging)
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
        if (merger != null)
        {
            merger.ResetMergerInfo();
        }
        QueueFree();
        MakeSplatter(originPos, .67f);
    }

    public virtual Splatter1 MakeSplatter(Vector2 originPos, float scale) //used to return the splatter
    {
        Splatter1 splatter = GibManager.getSplatter(this.Position, originPos.AngleToPoint(Position), Vector2.One * scale, gibColor);
        GetParent().CallDeferred(Node2D.MethodName.AddChild, splatter);
        return splatter;
    }

    private void _on_mouse_entered()
    {
        /*
        GD.Print("=====");
        GD.Print(this.Velocity);
        GD.Print(this.Velocity.Length());
        */
    }
}