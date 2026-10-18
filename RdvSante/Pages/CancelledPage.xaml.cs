using RdvSante.Services;

namespace RdvSante.Pages;

public partial class CancelledPage : ContentPage
{
    public CancelledPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var a = AppState.LastCancelled;
        if (a is null) return;
        InfoLabel.Text = $"Le {a.Practitioner.Name} a été informé. Vous recevrez également un email de confirmation.";
        DateLabel.Text = $"{a.DateLabel} · {a.StartTime}";
        DetailLabel.Text = $"{a.Practitioner.Name} · Consultation vidéo · {a.Practitioner.PriceLabel}";
    }

    protected override bool OnBackButtonPressed()
    {
        _ = Nav.ToHome();
        return true;
    }

    async void OnNew(object? sender, EventArgs e) => await Nav.ToHome();
}
