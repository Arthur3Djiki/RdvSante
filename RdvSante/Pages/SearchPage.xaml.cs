using RdvSante.Services;

namespace RdvSante.Pages;

public partial class SearchPage : ContentPage
{
    string _filter = "Tous";
    CancellationTokenSource? _cts;

    public SearchPage()
    {
        InitializeComponent();
        UpdateChips();
        _ = Refresh(600);
    }

    void UpdateChips()
    {
        ChipAll.Style = Ui.Get(_filter == "Tous" ? "ChipActive" : "Chip");
        ChipGen.Style = Ui.Get(_filter == "Généraliste" ? "ChipActive" : "Chip");
        ChipDerm.Style = Ui.Get(_filter == "Dermato" ? "ChipActive" : "Chip");
    }

    void ShowState(string state)
    {
        LoadingView.IsVisible = state == "loading";
        ResultsView.IsVisible = state == "list";
        EmptyView.IsVisible = state == "empty";
        ErrorView.IsVisible = state == "error";
    }

    async Task Refresh(int delayMs)
    {
        _cts?.Cancel();
        var cts = _cts = new CancellationTokenSource();
        ShowState("loading");
        try { await Task.Delay(delayMs, cts.Token); }
        catch (TaskCanceledException) { return; }

        var q = (SearchEntry.Text ?? "").Trim();
        if (q.Contains("erreur", StringComparison.OrdinalIgnoreCase))
        {
            ShowState("error");
            return;
        }

        var items = AppState.Practitioners.Where(p =>
            (_filter == "Tous" || p.Specialty.Contains(_filter, StringComparison.OrdinalIgnoreCase)) &&
            (q.Length == 0 ||
             p.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ||
             p.Specialty.Contains(q, StringComparison.OrdinalIgnoreCase))).ToList();

        CountLabel.Text = items.Count > 1 ? $"{items.Count} praticiens disponibles" : $"{items.Count} praticien disponible";
        BindableLayout.SetItemsSource(ListLayout, items);
        ShowState(items.Count == 0 ? "empty" : "list");
    }

    async void OnSearchChanged(object? sender, TextChangedEventArgs e) => await Refresh(300);

    async void OnChip(object? sender, EventArgs e)
    {
        _filter = (string)((Button)sender!).CommandParameter;
        UpdateChips();
        await Refresh(300);
    }

    async void OnClearFilters(object? sender, EventArgs e)
    {
        _filter = "Tous";
        UpdateChips();
        SearchEntry.Text = "";
        await Refresh(300);
    }

    async void OnRetry(object? sender, EventArgs e)
    {
        SearchEntry.Text = "";
        await Refresh(600);
    }

    async void OnCardTapped(object? sender, TappedEventArgs e)
    {
        if (sender is BindableObject b && b.BindingContext is Practitioner p)
        {
            AppState.StartBooking(p);
            await Nav.Go("practitioner");
        }
    }
}
