using RdvSante.Services;

namespace RdvSante.Pages;

public partial class ProfilePage : ContentPage
{
    bool _editing;

    public ProfilePage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Fill();
        DarkSwitch.IsToggled = Application.Current?.UserAppTheme == AppTheme.Dark;
    }

    void Fill()
    {
        FullName.Text = $"{AppState.LastName} {AppState.FirstName}";
        PrenomEntry.Text = AppState.FirstName;
        NomEntry.Text = AppState.LastName;
        EmailEntry.Text = AppState.Email;
        PhoneEntry.Text = AppState.Phone;
    }

    void SetEditable(bool editable)
    {
        _editing = editable;
        PrenomEntry.IsReadOnly = NomEntry.IsReadOnly = EmailEntry.IsReadOnly = PhoneEntry.IsReadOnly = !editable;
        EditLink.Text = editable ? "Enregistrer" : "Modifier mes informations";
    }

    void OnEdit(object? sender, TappedEventArgs e)
    {
        if (_editing)
        {
            if (!string.IsNullOrWhiteSpace(PrenomEntry.Text)) AppState.FirstName = PrenomEntry.Text.Trim();
            if (!string.IsNullOrWhiteSpace(NomEntry.Text)) AppState.LastName = NomEntry.Text.Trim();
            if (Ui.IsEmail(EmailEntry.Text)) AppState.Email = EmailEntry.Text!.Trim();
            if (!string.IsNullOrWhiteSpace(PhoneEntry.Text)) AppState.Phone = PhoneEntry.Text.Trim();
            Fill();
            SetEditable(false);
        }
        else
        {
            SetEditable(true);
            PrenomEntry.Focus();
        }
    }

    void OnDarkToggled(object? sender, ToggledEventArgs e)
    {
        DarkLabel.Text = e.Value ? "Activé" : "Désactivé";
        if (Application.Current is not null)
            Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }

    async void OnLogout(object? sender, EventArgs e) => await Nav.ToLogin();
}
