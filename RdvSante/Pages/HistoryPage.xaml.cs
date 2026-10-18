using RdvSante.Services;

namespace RdvSante.Pages;

public partial class HistoryPage : ContentPage
{
    public HistoryPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var items = AppState.History.ToList();
        BindableLayout.SetItemsSource(ListLayout, items);
        ListLayout.IsVisible = items.Count > 0;
        YearLabel.IsVisible = items.Count > 0;
        EmptyView.IsVisible = items.Count == 0;
        SubLabel.Text = items.Count > 0 ? "Retrouvez vos consultations passées." : "Vos consultations passées";
    }

    async void OnFind(object? sender, EventArgs e) => await Nav.ToHome();
}
