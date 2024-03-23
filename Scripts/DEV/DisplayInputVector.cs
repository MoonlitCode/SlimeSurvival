using System;
using System.Diagnostics;
using Godot;
using static SlimeSurvival.Scripts.GodotHelpers.StringInput;

namespace SlimeSurvival.Scripts.DEV;

public partial class DisplayInputVector : Node {
    [Export] private RichTextLabel _textLabel; 
    
    public override void _Process(double delta) {
        if (_textLabel is null) {
            Debug.Print("Missing 'RichTextLabel'");
            return;
        }
        
        base._Process(delta);

        var moveDirection = Input.GetVector(MoveLeft, MoveRight, MoveUp, MoveDown);
        _textLabel.Text = $"Vector2: ({MathF.Round(moveDirection.X, 3)}, {MathF.Round(moveDirection.Y, 3)})";
    }
}