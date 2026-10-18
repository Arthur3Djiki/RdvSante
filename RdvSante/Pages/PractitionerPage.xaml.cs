using RdvSante.Services;

namespace RdvSante.Pages;

public partial class PractitionerPage : ContentPage
{
    public PractitionerPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = AppState.Selected ?? AppState.Practitioners[0];
    }

    async void OnBack(object? sender, EventArgs e) => await Nav.Back();
    async void OnSlots(object? sender, EventArgs e) => await Nav.Go("slots");
}
