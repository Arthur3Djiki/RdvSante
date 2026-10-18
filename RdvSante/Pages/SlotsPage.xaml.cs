using RdvSante.Services;

namespace RdvSante.Pages;

public partial class SlotsPage : ContentPage
{
    public SlotsPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var p = AppState.Selected ??= AppState.Practitioners[0];
        HeaderPhoto.Source = p.Photo;
        HeaderName.Text = p.Name;
        HeaderSub.Text = $"Consultation vidéo · 20 min · {p.PriceLabel}";
        Render();
    }

    void Render()
    {
        BuildDays();
        BuildSlots();
        var chosen = AppState.Time is not null;
        ContinueBtn.IsEnabled = chosen;
        HintLabel.Text = chosen
            ? $"{AppState.DateLabel(AppState.DayIndex)} · {AppState.Time}"
            : "Sélectionnez un horaire pour continuer.";
    }

    void BuildDays()
    {
        DaysGrid.Children.Clear();
        for (int i = 0; i < 7; i++)
        {
            var active = i == AppState.DayIndex;
            var day = new Label { Text = AppState.DayShort[i], FontSize = 11, HorizontalOptions = LayoutOptions.Center };
            day.Style = Ui.Get("Muted");
            var num = new Label { Text = AppState.DayNumber[i].ToString(), FontSize = 16, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
            if (active) { day.TextColor = Colors.White; num.TextColor = Colors.White; }

            var box = new Border
            {
                Style = Ui.Get(active ? "SlotActive" : "Slot"),
                Padding = new Thickness(0, 8),
                Content = new VerticalStackLayout { Spacing = 2, Children = { day, num } },
            };
            int index = i;
            box.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    AppState.DayIndex = index;
                    AppState.Time = null;
                    Render();
                })
            });
            DaysGrid.Add(box, i, 0);
        }
    }

    void BuildSlots()
    {
        SlotsGrid.Children.Clear();
        int day = AppState.DayIndex;
        int free = 0;

        for (int t = 0; t < AppState.Times.Length; t++)
        {
            var time = AppState.Times[t];
            var available = AppState.IsAvailable(day, t);
            var selected = available && AppState.Time == time;
            if (available) free++;

            var label = new Label
            {
                Text = selected ? $"✓ {time}" : time,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = selected ? FontAttributes.Bold : FontAttributes.None,
            };
            if (!available)
            {
                label.TextDecorations = TextDecorations.Strikethrough;
                label.TextColor = Color.FromArgb("#8A9490");
            }
            else if (selected)
            {
                label.TextColor = Colors.White;
            }

            var box = new Border
            {
                Style = Ui.Get(!available ? "SlotOff" : selected ? "SlotActive" : "Slot"),
                Content = label,
            };
            if (available)
            {
                var chosen = time;
                box.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() =>
                    {
                        AppState.Time = chosen;
                        Render();
                    })
                });
            }
            SlotsGrid.Add(box, t % 3, t / 3);
        }

        DayTitle.Text = AppState.DateLabel(day);
        AvailLabel.Text = free > 1 ? $"Matin · {free} créneaux disponibles" : $"Matin · {free} créneau disponible";
    }

    async void OnBack(object? sender, EventArgs e) => await Nav.Back();
    async void OnContinue(object? sender, EventArgs e) => await Nav.Go("confirm");
}
