using System;
using Godot;

namespace SlimeSurvival.Scripts.Systems.Health;

public partial class HealthSystem : Node {
    public event EventHandler OnHealthChanged;
    public event EventHandler OnHealthDepleted;

    [Export] private float _maxHealth = -1;
    private float _currentHealth;

    public override void _Ready() {
        base._Ready();
        if (_maxHealth < 0) return;
        
        _currentHealth = _maxHealth;
        InvokeOnHealthChanged();
    }

    /// <summary>
    /// Will return if amount is '-'
    /// </summary>
    /// <param name="amount">The amount to increment by.</param>
    public void IncrementHealth(float amount) {
        if (_maxHealth < 0 || amount < 0) return;
        _currentHealth += amount;
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
        InvokeOnHealthChanged();
    }

    
    /// <summary>
    /// Will return if amount is '-'
    /// </summary>
    /// <param name="amount">The amount to decrement by.</param>
    public void DecrementHealth(float amount) {
        if (_maxHealth < 0 || amount < 0) return;
        
        _currentHealth -= amount;
        InvokeOnHealthChanged();
        
        if (_currentHealth >= 0) return;
        _currentHealth = 0;
        InvokeOnHealthDepletion();
    }

    public float GetHealthNormalized() {
        if (_currentHealth is 0) return 0;
        return _currentHealth / _maxHealth;
    }

#region EventInvokes

    private void InvokeOnHealthChanged() => OnHealthChanged?.Invoke(this, EventArgs.Empty);
    private void InvokeOnHealthDepletion() => OnHealthDepleted?.Invoke(this, EventArgs.Empty);

#endregion
}
