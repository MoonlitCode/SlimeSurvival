using Godot;
using SlimeSurvival.Scripts.GodotHelpers;

namespace SlimeSurvival.Scripts.Effects;

public static class SpawnScene {
    public static void Smoke(Node2D parentNode2D) {
        if (GD.Load<PackedScene>(NodeReferences.EffectSmoke).Instantiate() is not Node2D smokeNode2D) return;
        smokeNode2D.GlobalPosition = parentNode2D.GlobalPosition;
        parentNode2D.GetParent().AddChild(smokeNode2D);
    }

    public static CharacterBody2D Slime(Vector2 globalPosition) {
        if (GD.Load<PackedScene>(NodeReferences.Slime).Instantiate() is not CharacterBody2D slimeBody2D) return null;
        slimeBody2D.GlobalPosition = globalPosition;
        return slimeBody2D;
    }
}
