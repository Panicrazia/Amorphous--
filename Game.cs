using Godot;
using System;

public partial class Game : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private int _score;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Randomize();
        NewGame();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		if((GD.Randi() % 20)==0){
			SpawnMob();
		}
	}
	
	public void SpawnMob(){
		// Note: Normally it is best to use explicit types rather than the `var`
		// keyword. However, var is acceptable to use here because the types are
		// obviously Mob and PathFollow2D, since they appear later on the line.


		var mobSpawnLocation = GetNode<PathFollow2D>("spawnBorder/slimeSpawn");
		mobSpawnLocation.ProgressRatio = GD.Randf();

        Enemy mob = GetNode<SpawnPoint>("%SpawnPoint").TrySpawn().Instantiate<Enemy>();

        //Enemy mob = MobScene.Instantiate<Enemy>();

        // Set the mob's direction perpendicular to the path direction.
        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		mob.Position = mobSpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 6, Mathf.Pi / 6);
		mob.Rotation = direction;

        // Choose the velocity.
        //var velocity = new Vector2((float)GD.RandRange(75.0, 100.0), 0);
        var velocity = new Vector2(SlimeConstants.GreenSlimeMaxSpeed, 0);
		mob.Velocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);
	}

	public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
	}

	public void NewGame()
	{
		_score = 0;
		GD.Print("start");
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("playerSpawn");
		player.Start(startPosition.Position);

		GetNode<Timer>("StartTimer").Start();

        Enemy mob = MobScene.Instantiate<Enemy>();
        mob.Position = new Vector2(100, 100);
		mob.Velocity = new Vector2(100, 0).Rotated(0);
        AddChild(mob);

        mob = MobScene.Instantiate<Enemy>();
        mob.Position = new Vector2(300, 100);
        mob.Velocity = new Vector2(100, 0).Rotated(Mathf.Pi);
        AddChild(mob);
    }
}

