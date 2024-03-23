namespace SlimeSurvival.Scripts.GodotHelpers;

public static class StringInput {

#region Movement

    public static string MoveUp { get; private set; } = "move_up";
    public static string MoveDown { get; private set; } = "move_down";
    public static string MoveRight { get; private set; } = "move_right";
    public static string MoveLeft { get; private set; } = "move_left";

#endregion

#region Actions

    public static string ActionShoot { get; private set; } = "action_shoot";
    public static string QuickRestart { get; private set; } = "function_quickRestart";

#endregion
}