using Godot;
using NodeReferences = SlimeSurvival.Scripts.GodotHelpers.NodeReferences;
using StringInput = SlimeSurvival.Scripts.GodotHelpers.StringInput;

namespace SlimeSurvival.Scripts.Equipment;

public partial class EquipmentPistol : Area2D {
    [Export] private Marker2D _firePoint;
    private bool _hasTargets;
    
    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        GetEnemyTargets();
    }

    public override void _Process(double delta) {
        base._Process(delta);
        TryFireProjectile();
    }

    private void TryFireProjectile() {
        if (!Input.IsActionJustPressed(StringInput.ActionShoot)) return;
        var projectileScene = GD.Load<PackedScene>(NodeReferences.Bullet);
        var projectileInstance = projectileScene.Instantiate<Area2D>();
        projectileInstance.GlobalPosition = _firePoint.GlobalPosition;
        projectileInstance.GlobalRotation = _firePoint.GlobalRotation;
        _firePoint.AddChild(projectileInstance);
    }

    private void GetEnemyTargets() {
        var enemiesInRange = GetOverlappingBodies();
        if (enemiesInRange.Count <= 0) {
            Rotation = 0;
            return;
        }
        
        var enemyTargeted = enemiesInRange[0];
        LookAt(enemyTargeted.GlobalPosition);
    }
}