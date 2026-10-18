using RdvSante.Services;

namespace RdvSante.Pages;

public partial class CancelPage : ContentPage
{
    public CancelPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (AppState.ToCancel is not null) Card.BindingContext = AppState.ToCancel;
    }

    async void OnBack(object? sender, EventArgs e) => await Nav.Back();
    async void OnKeep(object? sender, EventArgs e) => await Nav.Back();

    async void OnConfirmCancel(object? sender, EventArgs e)
    {
        AppState.CancelAppointment();
        await Nav.Go("cancelled");
    }
}
