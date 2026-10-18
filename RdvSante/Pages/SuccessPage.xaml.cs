using RdvSante.Services;

namespace RdvSante.Pages;

public partial class SuccessPage : ContentPage
{
    public SuccessPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var a = AppState.LastBooked;
        if (a is null) return;
        Card.BindingContext = a;
        EmailLabel.Text = $"Un email de confirmation a été envoyé à {AppState.Email}.";
        ReminderLabel.Text = a.Reminder == "Aucun"
            ? "Aucun rappel activé. Retrouvez le lien vidéo dans Mes rendez-vous, quelques minutes avant la consultation."
            : $"Rappel activé : {a.Reminder} avant. Retrouvez le lien vidéo dans Mes rendez-vous, quelques minutes avant la consultation.";
    }

    // Le bouton Retour système ne doit pas revenir sur l'écran de confirmation (risque de double réservation).
    protected override bool OnBackButtonPressed()
    {
        _ = Nav.ToAppointments();
        return true;
    }

    async void OnSeeAppointments(object? sender, EventArgs e) => await Nav.ToAppointments();
}
