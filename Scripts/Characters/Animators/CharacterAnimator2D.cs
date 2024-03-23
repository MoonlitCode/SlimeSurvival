using System;
using Godot;
using static SlimeSurvival.Scripts.GodotHelpers.StringAnimations;

namespace SlimeSurvival.Scripts.Characters.Animators;

public partial class CharacterAnimator2D : Node {
    public event EventHandler<bool> IsHurt; 
    
    [Export] private AnimationPlayer _animationPlayer;

    public override void _Ready() {
        base._Ready();
        _animationPlayer.AnimationChanged += Player_CurrentAnimationChanged;
    }

    public override void _ExitTree() {
        base._ExitTree();
        _animationPlayer.AnimationChanged -= Player_CurrentAnimationChanged;
    }
    
    private void Player_CurrentAnimationChanged(StringName oldname, StringName newname) {
        if (newname != ActionHurt) InvokeIsHurtPlaying(false);
    }

    public void PlayIdleAnimation() => _animationPlayer.Play(ActionIdle);
    public void PlayWalkAnimation() => _animationPlayer.Play(ActionWalk);

    public void PlayHurtAnimation() {
        InvokeIsHurtPlaying(true);
        _animationPlayer.Play(ActionHurt);
        _animationPlayer.Queue(ActionIdle);
    }

    private void InvokeIsHurtPlaying(bool state) => IsHurt?.Invoke(this, state);
}
