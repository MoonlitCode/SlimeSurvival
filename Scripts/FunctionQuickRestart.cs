using Godot;
using static SlimeSurvival.Scripts.GodotHelpers.StringInput;

namespace SlimeSurvival.Scripts;

public partial class FunctionQuickRestart : Node {
    public override void _Process(double delta) {
        base._Process(delta);
        if (Input.IsActionPressed(QuickRestart)) GetTree().ReloadCurrentScene();
    }
}