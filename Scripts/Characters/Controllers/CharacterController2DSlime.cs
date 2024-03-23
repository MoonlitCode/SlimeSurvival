using System;
using Godot;
using SlimeSurvival.Scripts.Effects;
using SlimeSurvival.Scripts.Systems.Health;

namespace SlimeSurvival.Scripts.Characters.Controllers;

public partial class CharacterController2DSlime : CharacterController2D {
	[Export] private HealthSystem _healthSystem;
	private Vector2 _lastKnownTargetPosition;
	private bool _isHurt;
	
	public override void _Ready() {
		base._Ready();
		characterAnimator2D.IsHurt += Animator2D_OnIsHurt;
		_healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
		_healthSystem.OnHealthDepleted += HealthSystem_OnHealthDepleted;
	}

	public override void _ExitTree() {
		base._ExitTree();
		characterAnimator2D.IsHurt -= Animator2D_OnIsHurt;
		_healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
		_healthSystem.OnHealthDepleted -= HealthSystem_OnHealthDepleted;
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
		if (Game.Instance == null) return;
		var playerCharacter = Game.Instance.GetPlayerCharacter();
		if (_isHurt) return;
		HandleMovement(GetPlayerPosition(playerCharacter));
	}
	
	public override void _Process(double delta) {
		base._Process(delta);
		if (_isHurt) return;
		HandleAnimation();
	}
	
	private void HealthSystem_OnHealthDepleted(object sender, EventArgs e) {
		SpawnScene.Smoke(characterBody2D);
		characterBody2D.QueueFree();
	}

	private void HealthSystem_OnHealthChanged(object sender, EventArgs e) => characterAnimator2D.PlayHurtAnimation();

	private void Animator2D_OnIsHurt(object sender, bool state) => _isHurt = state;

	protected override void HandleAnimation() {
		base.HandleAnimation();
		characterAnimator2D.PlayIdleAnimation();
	}

	private Vector2 GetPlayerPosition(Node2D playerCharacter) {
		if (playerCharacter is not null) {
			_lastKnownTargetPosition = characterBody2D.GlobalPosition.DirectionTo(playerCharacter.GlobalPosition);
			return _lastKnownTargetPosition;
		}

		var offScreenTarget = _lastKnownTargetPosition * -1 * 100;
		return offScreenTarget;
	}
}
