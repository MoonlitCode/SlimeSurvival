#nullable enable

using System;
using Godot;

namespace SlimeSurvival.Scripts.Systems.Health;

public partial class HealthUI : Node {
    [Export] private ProgressBar? _healthBar;
    [Export] private HealthSystem? _healthSystem;
    
    public override void _Ready() {
        base._Ready();
        if (_healthBar is null || _healthSystem is null) {
            QueueFree();
            return;
        }
        
        _healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        UpdateHealthBar();
    }
    
    private void HealthSystem_OnHealthChanged(object? sender, EventArgs e) => UpdateHealthBar();

    private void UpdateHealthBar() => _healthBar.Value = _healthSystem.GetHealthNormalized();
}
