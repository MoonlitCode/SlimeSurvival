#nullable enable

using System;
using Godot;
using SlimeSurvival.Scripts.Systems.Health;
using static SlimeSurvival.Scripts.GodotHelpers.StringInput;

namespace SlimeSurvival.Scripts.Characters.Controllers;

public partial class CharacterController2DPlayer : CharacterController2D {
	[Export] private HealthSystem? _healthSystem;

	public override void _Ready() {
		base._Ready();
		if (_healthSystem is null) return;
		_healthSystem.OnHealthDepleted += HealthSystem_OnHealthDepleted;
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (_healthSystem is null) return;
		_healthSystem.OnHealthDepleted -= HealthSystem_OnHealthDepleted;
	}

	public override void _PhysicsProcess(double delta) {
		base._Process(delta);
		var moveDirection = GetUserInputVector2();
		HandleMovement(moveDirection);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		HandleAnimation();
	}
	
	private void HealthSystem_OnHealthDepleted(object? sender, EventArgs e) => GD.Print($"Health Depleted");

	protected override void HandleAnimation() {
		base.HandleAnimation();
		
		if (IsCharacterMoving()) characterAnimator2D.PlayWalkAnimation();
		else characterAnimator2D.PlayIdleAnimation();
	}

#region GetsAndSets

	private Vector2 GetUserInputVector2() 
		=> Input.GetVector(MoveLeft, MoveRight, MoveUp, MoveDown);

#endregion
}
