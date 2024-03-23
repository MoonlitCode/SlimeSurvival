#nullable enable
using System;
using Godot;

namespace SlimeSurvival.Scripts.Characters;

public partial class SpawnTimer : Node {
    public event EventHandler? OnTimerComplete;
    
    [Export] private float _timerValue;
    private float _timerValueCurrent;
    private bool _isTimerComplete;

    public override void _Ready() {
        base._Ready();
        ResetTimer();
    }

    public override void _Process(double delta) {
        base._Process(delta);
        if (_isTimerComplete) return;
        _timerValueCurrent -= (float)delta;
        
        if (_timerValueCurrent > 0) return;
        CompleteTimer();
    }

    public void ResetTimer() {
        _isTimerComplete = false;
        _timerValueCurrent = _timerValue;
    }

    public void CompleteTimer() {
        _isTimerComplete = true;
        _timerValueCurrent = 0;
        OnTimerComplete?.Invoke(this, EventArgs.Empty);
    }

    public bool GetIsTimerComplete() => _isTimerComplete;
}
