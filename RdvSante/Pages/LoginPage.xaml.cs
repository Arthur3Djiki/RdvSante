using RdvSante.Services;

namespace RdvSante.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage() => InitializeComponent();

    void OnTogglePwd(object? sender, TappedEventArgs e)
    {
        PwdEntry.IsPassword = !PwdEntry.IsPassword;
        TogglePwd.Text = PwdEntry.IsPassword ? "Afficher" : "Masquer";
    }

    async void OnLogin(object? sender, EventArgs e)
    {
        var emailOk = Ui.IsEmail(EmailEntry.Text);
        var pwdOk = !string.IsNullOrEmpty(PwdEntry.Text);
        Ui.SetState(EmailBox, EmailError, emailOk ? null : "Saisissez une adresse email valide.");
        Ui.SetState(PwdBox, PwdError, pwdOk ? null : "Saisissez votre mot de passe.");
        if (!emailOk || !pwdOk) return;

        AppState.Email = EmailEntry.Text!.Trim();
        PwdEntry.Text = "";
        await Nav.ToHome();
    }

    async void OnRegister(object? sender, EventArgs e) => await Nav.Go("register");
}
