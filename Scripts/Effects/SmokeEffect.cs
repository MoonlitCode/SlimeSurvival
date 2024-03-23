#nullable enable

using Godot;
using SlimeSurvival.Scripts.GodotHelpers;

namespace SlimeSurvival.Scripts.Effects;

public partial class SmokeEffect : Node2D {
    [Export] private AnimationPlayer? _animationPlayer;
    [Export] private ColorRect? _colorRect;

    public override void _Ready() {
        base._Ready();
        if (_animationPlayer is null || _colorRect is null) return;
        
        _animationPlayer.AnimationFinished += AnimationPlayer_AnimationFinished;
        
        (_colorRect.Material as ShaderMaterial)?.SetShaderParameter(
            "texture_offset", 
            new Vector2(GD.Randf(), GD.Randf())
            );
        _animationPlayer.Play(StringAnimations.SmokeExplosion);
    }

    public override void _ExitTree() {
        base._ExitTree();
        if (_animationPlayer is null) return;
        _animationPlayer.AnimationFinished -= AnimationPlayer_AnimationFinished;
    }

    private void AnimationPlayer_AnimationFinished(StringName animname) => QueueFree();
}

