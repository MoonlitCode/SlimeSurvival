using Godot;
using HealthSystem = SlimeSurvival.Scripts.Systems.Health.HealthSystem;

namespace SlimeSurvival.Scripts;

public partial class ProjectileBullet : Area2D {
    [Export] private HealthSystem _healthSystem;
    [Export] private float _moveSpeed = 600;
    [Export] private float _range = 800;
    private float _currentRange;

    public override void _Ready() {
        base._Ready();
        BodyEntered += Event_OnBodyEntered;
    }

    public override void _ExitTree() {
        base._ExitTree();
        BodyEntered -= Event_OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        HandleMovement(delta);
        HandleRangeDistance(delta);
    }
    
    private void Event_OnBodyEntered(Node2D body) {
        // Only here to get the Node name.
        if (_healthSystem is null) return;
        if (body.GetNode($"{_healthSystem.Name}") is not HealthSystem targetHealthSystem) return;

        targetHealthSystem.DecrementHealth(2);
        QueueFree();
    }
    
    private void HandleMovement(double delta) {
        var moveDirection = Vector2.Right.Rotated(Rotation);
        // 'Delta' applied here because 'MoveAndSlide()' applies it by itself.
        Position += moveDirection * _moveSpeed * (float)delta;
    }

    private void HandleRangeDistance(double delta) {
        _currentRange += _moveSpeed * (float)delta;
        if (_currentRange >= _range) QueueFree();
    }

    public void SetRange(int range) => _range = range;
}