#nullable enable

using System;
using Godot;
using SlimeSurvival.Scripts.Characters;
using SlimeSurvival.Scripts.DEV;
using SlimeSurvival.Scripts.Effects;
using SlimeSurvival.Scripts.Systems.Health;

namespace SlimeSurvival.Scripts;

public partial class Game :Node2D {
	public static Game? Instance;

	[Export] private CharacterBody2D? _playerCharacterBody2D;
	[Export] private HealthSystem? _healthSystem;
	[Export] private SpawnTimer? _spawnTimer;
	[Export] private SpawnPoints? _spawnPoints;
	[Export] private DisplayGameOver? _displayGameOver;

	public override void _Ready() {
		base._Ready();
		Instance = this;

		if (_healthSystem is not null)
			_healthSystem.OnHealthDepleted += PlayerHealth_OnHealthDepleted;
		if (_spawnTimer is not null && _spawnPoints is not null)
			_spawnTimer.OnTimerComplete += SpawnTimer_OnTimerComplete;
		
		_spawnTimer?.ResetTimer();
	}

	public override void _ExitTree() {
		base._ExitTree();
		Instance = null;
		
		if (_healthSystem is not null)
			_healthSystem.OnHealthDepleted -= PlayerHealth_OnHealthDepleted;
		if (_spawnTimer is not null && _spawnPoints is not null)
			_spawnTimer.OnTimerComplete -= SpawnTimer_OnTimerComplete;
	}
	
	private void PlayerHealth_OnHealthDepleted(object? sender, EventArgs e) {
		_playerCharacterBody2D?.QueueFree();
		_playerCharacterBody2D = null;
		_displayGameOver?.StartTimer();
	}
	
	private void SpawnTimer_OnTimerComplete(object? sender, EventArgs e) {
		if (_spawnPoints is null) return;
		var slimeBody = SpawnScene.Slime(_spawnPoints.GetRandomSpawnPoint().GlobalPosition);
		
		if (slimeBody is null) return;
		_spawnTimer?.ResetTimer();
		AddChild(slimeBody);
	}

	public void RestartGame() => GetTree().ReloadCurrentScene();
	
	public CharacterBody2D? GetPlayerCharacter() => _playerCharacterBody2D;
}