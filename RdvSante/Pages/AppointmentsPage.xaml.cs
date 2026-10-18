using RdvSante.Services;

namespace RdvSante.Pages;

public partial class AppointmentsPage : ContentPage
{
    public AppointmentsPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var items = AppState.Upcoming.ToList();
        BindableLayout.SetItemsSource(ListLayout, items);
        ListLayout.IsVisible = items.Count > 0;
        EmptyView.IsVisible = items.Count == 0;
        SubLabel.Text = items.Count switch
        {
            0 => "Vos prochaines consultations",
            1 => "1 rendez-vous à venir",
            _ => $"{items.Count} rendez-vous à venir",
        };
    }

    async void OnCancel(object? sender, EventArgs e)
    {
        if (sender is BindableObject b && b.BindingContext is Appointment a)
        {
            AppState.ToCancel = a;
            await Nav.Go("cancel");
        }
    }

    async void OnFind(object? sender, EventArgs e) => await Nav.ToHome();
}
