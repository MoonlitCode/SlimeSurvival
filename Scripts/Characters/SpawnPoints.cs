using Godot;
using Godot.Collections;

namespace SlimeSurvival.Scripts.Characters;

public partial class SpawnPoints : Node2D {
    [Export] private Array<Node2D> _spawnPoints;
    [Export] private float _rotationSpeed = 3f;

    public override void _Process(double delta) {
        base._Process(delta);
        RotationDegrees += _rotationSpeed;
    }

    public Array<Node2D> GetSpawnPoints() => _spawnPoints;
    public Node2D GetRandomSpawnPoint() 
        => _spawnPoints[GD.RandRange(0, _spawnPoints.Count - 1)];
}