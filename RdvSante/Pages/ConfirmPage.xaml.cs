using RdvSante.Services;

namespace RdvSante.Pages;

public partial class ConfirmPage : ContentPage
{
    string _reminder = "24 h";

    public ConfirmPage()
    {
        InitializeComponent();
        UpdateReminder();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (AppState.Selected is null || AppState.Time is null) return;
        Card.BindingContext = AppState.Draft(_reminder, MotifEditor.Text ?? "");
    }

    void UpdateReminder()
    {
        Rem24.Style = Ui.Get(_reminder == "24 h" ? "ChipActive" : "Chip");
        Rem1.Style = Ui.Get(_reminder == "1 h" ? "ChipActive" : "Chip");
        RemNone.Style = Ui.Get(_reminder == "Aucun" ? "ChipActive" : "Chip");
        ReminderHint.Text = _reminder == "Aucun"
            ? "Aucun rappel ne sera envoyé."
            : $"Un rappel sera envoyé par email {_reminder} avant.";
    }

    void OnReminder(object? sender, EventArgs e)
    {
        _reminder = (string)((Button)sender!).CommandParameter;
        UpdateReminder();
    }

    async void OnBack(object? sender, EventArgs e) => await Nav.Back();

    async void OnConfirm(object? sender, EventArgs e)
    {
        if (AppState.Selected is null || AppState.Time is null)
        {
            await Nav.ToHome();
            return;
        }
        AppState.Book(_reminder, MotifEditor.Text ?? "");
        await Nav.Go("success");
    }
}
