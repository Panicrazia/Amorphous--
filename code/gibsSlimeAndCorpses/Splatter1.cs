using Godot;
using System;

public partial class Splatter1 : CanvasLayer
{
	public bool fading = true;
	public CanvasModulate modulate;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        modulate = GetNode<CanvasModulate>("%CanvasModulate");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        //CanvasLayer layer = GetNode<CanvasLayer>("%CanvasLayer");
        //GD.Print(layer.Rotation);
    }

	public override void _PhysicsProcess(double delta)
	{
		if (fading)
		{
			Color mod = modulate.Color;
			//GD.Print(mod.A);
            modulate.Color = new Color(mod, mod.A - .007f);
			if(mod.A <= 0)
			{
				this.Kill();
			}
        }
	}

	public virtual void Kill()
	{
		this.QueueFree();
	}

    public void SetPosRotScale(Vector2 pos, float rot, Vector2 scal)
	{
		//CanvasLayer layer = GetNode<CanvasLayer>("%CanvasLayer");
		Offset = pos;
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
}
