using Godot;

namespace SlimeSurvival.Scripts.DEV;

public partial class DisplayGameOver : Control {
    [Export] private Label? _countDownLabel;
    [Export] private Timer? _countDownTimer;
    private bool _isCounting;

    public override void _Ready() {
        base._Ready();
        Visible = false;
        if (_countDownTimer is null) return;
        _isCounting = false;
        _countDownTimer.Timeout += Timer_Timeout;
    }

    public override void _Process(double delta) {
        base._Process(delta);
        if (_isCounting) UpdateCountDownLabel();
    }

    private void Timer_Timeout()  => Game.Instance?.RestartGame();

    private void UpdateCountDownLabel() {
        if (_countDownLabel is null || _countDownTimer is null) return;
        var timeLeft = Mathf.Round(_countDownTimer.TimeLeft);
        _countDownLabel.Text = $"{Mathf.Round(timeLeft)}";
    }

    public void StartTimer() {
        Visible = true;
        _countDownTimer?.Start();
        _isCounting = true;
    }
}
