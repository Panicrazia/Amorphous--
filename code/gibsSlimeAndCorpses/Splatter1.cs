using Godot;
using System;

public partial class Splatter1 : Node2D
{
	public bool fading = true;
    public float stickyness = 0; //how long it will remain before fading
	public StatusEnum statusToInflict = StatusEnum.None;
    public PackedScene gooScene = GD.Load<PackedScene>("res://code/gibsSlimeAndCorpses/YellowGoo.tscn");
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        //CanvasLayer layer = GetNode<CanvasLayer>("%CanvasLayer");
        //GD.Print(layer.Rotation);
    }

	public override void _PhysicsProcess(double delta)
	{
		if(stickyness > 0)
		{
			stickyness -= 1f; //1 * delta?
		}
		else
		{
            if (fading)
            {
                Color mod = Modulate;
                //GD.Print(mod.A);
                Modulate = new Color(mod, mod.A - .007f); //.007f * delta?
                if (mod.A <= 0)
                {
                    this.Kill();
                }
            }
        }
		
	}

	public virtual void Kill()
	{
		this.QueueFree();
	}

	public virtual void SetModulateColor(Color mod)
	{
		this.Modulate = mod;
	}

    public void SetPosRotScale(Vector2 pos, float rot, Vector2 scal)
	{
		//CanvasLayer layer = GetNode<CanvasLayer>("%CanvasLayer");
		Position = pos;
        Rotation = rot;
		Scale = scal;

        /*
		 * 
		CanvasLayer layer = GetNode<CanvasLayer>("%CanvasLayer");
		layer.Offset = pos;
        layer.Rotation = rot;
		layer.Scale = scal;

		 */
        //GD.Print(layer.Rotation);
    }

	public void SetSticky(float stickyVal)
	{
		this.stickyness = stickyVal;
	}

    public void SetNoFade()
    {
        fading = false;
    }

	public virtual void SetStatusToInflict(StatusEnum status)
	{
		Area2D area = (GetNode("Hitbox") as Area2D);
        area.Monitorable = true;
        area.Monitoring = true;
		statusToInflict = status;
		//area.Connect();
		area.BodyEntered += InflictStatus;
        area.AreaEntered += InflictStatus;
    }

	public virtual void InflictStatus(Node2D body)
	{
		GD.Print("stepppp");

        IStatusAffected statuser = body.GetParent() as IStatusAffected;

        if (statuser is Player)
		{
			GD.Print(" player ");
            //statuser = (body as Player);
        }

        if (statuser != null)
        {
            GD.Print("a second print statement has hit the console mr president");
			if(statuser.AddStatus(statusToInflict, statusToInflict.GetDefaultDuration()))
			{
				body.GetParent().AddChild(gooScene.Instantiate());
				GD.Print("called");
			}
		}
		//probably a switch statement for acid to just damage them 
		//...or acid should work like a status effect that damages you and turns the sprite more orange
		//and the longer you stay in acid the more acidy it becomes (stacking durration, and acid does more damage for the duration of it?)
	}
}
