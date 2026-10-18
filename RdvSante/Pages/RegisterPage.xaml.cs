using RdvSante.Services;

namespace RdvSante.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage() => InitializeComponent();

    async void OnBack(object? sender, EventArgs e) => await Nav.Back();
    async void OnLoginTapped(object? sender, TappedEventArgs e) => await Nav.Back();

    async void OnCreate(object? sender, EventArgs e)
    {
        var nom = NomEntry.Text?.Trim();
        var prenom = PrenomEntry.Text?.Trim();
        var emailOk = Ui.IsEmail(EmailEntry.Text);
        var phoneOk = Ui.IsCameroonPhone(PhoneEntry.Text);
        var pwdOk = (PwdEntry.Text?.Length ?? 0) >= 8;
        var pwd2Ok = pwdOk && PwdEntry.Text == Pwd2Entry.Text;
        var termsOk = TermsBox.IsChecked;

        Ui.SetState(NomBox, NomMsg, string.IsNullOrEmpty(nom) ? "Saisissez votre nom." : null, "✓ Nom renseigné.");
        Ui.SetState(PrenomBox, PrenomMsg, string.IsNullOrEmpty(prenom) ? "Saisissez votre prénom." : null, "✓ Prénom renseigné.");
        Ui.SetState(EmailBox, EmailMsg, emailOk ? null : "Saisissez une adresse email valide.");
        Ui.SetState(PhoneBox, PhoneMsg, phoneOk ? null : "Saisissez un numéro camerounais valide.");
        Ui.SetState(PwdBox, PwdMsg, pwdOk ? null : "Utilisez au moins 8 caractères.");
        Ui.SetState(Pwd2Box, Pwd2Msg, pwd2Ok ? null : "Les mots de passe ne correspondent pas.");
        TermsMsg.Style = Ui.Get("ErrorText");
        TermsMsg.Text = "Acceptez les conditions pour continuer.";
        TermsMsg.IsVisible = !termsOk;

        var valid = !string.IsNullOrEmpty(nom) && !string.IsNullOrEmpty(prenom) && emailOk && phoneOk && pwdOk && pwd2Ok && termsOk;
        SubTitle.Text = valid ? "Vos informations personnelles" : "Vérifiez les champs signalés ci-dessous.";
        if (!valid) return;

        AppState.LastName = nom!;
        AppState.FirstName = prenom!;
        AppState.Email = EmailEntry.Text!.Trim();
        AppState.Phone = PhoneEntry.Text!.Trim();
        await Nav.ToHome();
    }
}
