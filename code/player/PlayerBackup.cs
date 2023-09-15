using Godot;
using System;
using System.Collections.Generic;

public partial class Playerb : Area2D//, IStatusAffected
{
    [Signal]
    public delegate void HitEventHandler();

    public float speed
    {
        get
        {
            if (statuses.ContainsKey(StatusEnum.StickyGoo))
            {
                return speed * .4f;
            }
            else
            {
                return speed;
            }
        }
        set => speed = value;
    }
    public Dictionary<StatusEnum, int> statuses { get => statuses; set => statuses = value; }

    public Vector2 ScreenSize; // Size of the game window.

    public bool isAttacking = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        Engine.RegisterSingleton("Player1", this);
        speed = 300f;
    }

    public override void _Process(double delta)
    {
    }
    /*
    public Player GetPlayer()
    {
        return this;
    }*/

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (!isAttacking)
        {
            LookAt(GetGlobalMousePosition());

            Vector2 velocity = Vector2.Zero; // The player's movement vector.

            if (Input.IsActionPressed("moveRight"))
            {
                velocity.X += 1;
            }

            if (Input.IsActionPressed("moveLeft"))
            {
                velocity.X -= 1;
            }

            if (Input.IsActionPressed("moveDown"))
            {
                velocity.Y += 1;
            }

            if (Input.IsActionPressed("moveUp"))
            {
                velocity.Y -= 1;
            }

            if (Input.IsActionPressed("click"))
            {
                Attack();
            }


            if (velocity.Length() > 0)
            {
                velocity = velocity.Normalized() * speed;
            }
            else
            {
            }

            Position += velocity * (float)delta;
            Position = new Vector2(
                x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
                y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
            );
        }
    }

    public void Attack()
    {
        GetWeapon().Attack();
        isAttacking = true;
    }

    public void AttackEnd()
    {
        isAttacking = false;
    }

    public Weapon GetWeapon()
    {
        return GetNode<Node2D>("Weapon") as Weapon;
    }

    private void _on_body_entered(Node2D body)
    {
        Hide(); // Player disappears after being hit.
        EmitSignal(SignalName.Hit);
        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }

    public void Start(Vector2 position)
    {
        Position = position;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }
}


