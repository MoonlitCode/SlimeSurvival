using Godot;
using CharacterAnimator2D = SlimeSurvival.Scripts.Characters.Animators.CharacterAnimator2D;

namespace SlimeSurvival.Scripts.Characters.Controllers;

public partial class CharacterController2D : Node {
    [Export] protected CharacterBody2D characterBody2D;
    [Export] protected CharacterAnimator2D characterAnimator2D;
    [Export] private float _moveSpeed = 300.0f;
    private Vector2 _currentMoveDirection;

    protected virtual void HandleMovement(Vector2 moveDirection) {
        _currentMoveDirection = moveDirection;
        characterBody2D.Velocity = moveDirection * _moveSpeed;
        // Don't fully understand how this works, but it drastically lowers the speed when the player disappears.
        characterBody2D.Velocity = characterBody2D.Velocity.Normalized() * Mathf.Min(characterBody2D.Velocity.Length(), _moveSpeed);
        characterBody2D.MoveAndSlide();
    }

    protected virtual void HandleAnimation() {
        if (characterAnimator2D is not null) return;
    }

    protected bool IsCharacterMoving() {
        return _currentMoveDirection.Length() > 0;
    }
}
