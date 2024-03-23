#nullable enable

using Godot;

namespace SlimeSurvival.Scripts.Systems.Health;

public partial class DamageOverTime : Node {
    [Export] private Area2D? _hurtBox;
    [Export] private HealthSystem? _healthSystem;
    [Export] private float _damageAmount = 1;

    public override void _Process(double delta) {
        if (_hurtBox is null || _healthSystem is null) return;
        base._Process(delta);

        var overlappingBodies= _hurtBox.GetOverlappingBodies().Count;
        if (overlappingBodies <= 0) return;
        var damage= overlappingBodies * _damageAmount * (float)delta;
        _healthSystem.DecrementHealth(damage);
    }
}
