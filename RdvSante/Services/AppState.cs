namespace RdvSante.Services;

public class Practitioner
{
    public string Name { get; init; } = "";
    public string Specialty { get; init; } = "";
    public string City { get; init; } = "";
    public string Photo { get; init; } = "";
    public string Cabinet { get; init; } = "";
    public string Bio { get; init; } = "";
    public string NextSlot { get; init; } = "";
    public int Price { get; init; } = 5000;

    public string Subtitle => $"{City} · Consultation vidéo";
    public string PriceLabel => $"{Price} XAF";
}

public class Appointment
{
    public Practitioner Practitioner { get; init; } = null!;
    public int DayIndex { get; init; }
    public string StartTime { get; init; } = "";
    public string Reminder { get; init; } = "24 h";
    public string Motif { get; init; } = "";

    public string DateLabel => $"{AppState.DateLabel(DayIndex)} 2026";
    public string TimeLabel
    {
        get
        {
            var end = TimeSpan.Parse(StartTime).Add(TimeSpan.FromMinutes(20));
            return $"{StartTime} – {end:hh\\:mm} · Heure du Cameroun";
        }
    }
    public string ReminderText => Reminder == "Aucun" ? "Aucun rappel activé" : "Rappel par email activé";
    public string ReminderSub => Reminder == "Aucun" ? "Vous ne recevrez pas de rappel." : $"{Reminder} avant la consultation";
}

public class HistoryItem
{
    public string Header { get; init; } = "";
    public string Status { get; init; } = "Terminé";
    public Practitioner Practitioner { get; init; } = null!;
    public string Detail { get; init; } = "";

    public bool IsCancelled => Status == "Annulé";
    public Color BadgeBg => IsCancelled ? Color.FromArgb("#FDECEA") : Color.FromArgb("#E1F3EE");
    public Color BadgeFg => IsCancelled ? Color.FromArgb("#C0392B") : Color.FromArgb("#0F766E");
    public string Subtitle => $"{Practitioner.Specialty} · {Practitioner.PriceLabel}";
}

/// <summary>Données de démonstration + état partagé entre les écrans (pas de back-end).</summary>
public static class AppState
{
    // Utilisateur
    public static string FirstName = "Jean";
    public static string LastName = "Essomba";
    public static string Email = "jean.essomba@exemple.fr";
    public static string Phone = "+237 6 XX XX XX XX";

    // Praticiens
    public static readonly List<Practitioner> Practitioners = new()
    {
        new Practitioner { Name = "Dr Salam Bahsoun", Specialty = "Médecin généraliste", City = "Douala", Photo = "doc1",
            Cabinet = "Cabinet médical Voltaire", NextSlot = "Jeudi 8 octobre · 09:30",
            Bio = "Le Dr Bahsoun accompagne les adultes pour les soins courants, le suivi médical et les conseils de prévention." },
        new Practitioner { Name = "Dr Essomba Martin", Specialty = "Médecin généraliste", City = "Douala", Photo = "doc2",
            Cabinet = "Cabinet médical du Littoral", NextSlot = "Jeudi 8 octobre · 11:00",
            Bio = "Le Dr Essomba accompagne les adultes pour les soins courants, le suivi médical et la prévention." },
        new Practitioner { Name = "Dr Ndom Solange", Specialty = "Dermatologue", City = "Yaoundé", Photo = "doc3",
            Cabinet = "Cabinet de dermatologie", NextSlot = "Vendredi 9 octobre · 14:00",
            Bio = "Le Dr Ndom accompagne les patients pour les soins de la peau, le suivi dermatologique et la prévention." },
    };

    // Rendez-vous
    public static readonly List<Appointment> Upcoming = new();
    public static readonly List<HistoryItem> History = new()
    {
        new HistoryItem { Header = "24 septembre 2026 · 09:30", Status = "Terminé", Practitioner = Practitioners[0], Detail = "Consultation vidéo · Suivi médical" },
        new HistoryItem { Header = "3 septembre 2026 · 10:00", Status = "Annulé", Practitioner = Practitioners[0], Detail = "Annulé par vous · Aucun frais" },
        new HistoryItem { Header = "13 août 2026 · 14:30", Status = "Terminé", Practitioner = Practitioners[0], Detail = "Consultation vidéo · Prévention" },
    };

    // Parcours de réservation en cours
    public static Practitioner? Selected;
    public static int DayIndex = 3;
    public static string? Time;
    public static Appointment? LastBooked;
    public static Appointment? ToCancel;
    public static Appointment? LastCancelled;

    // Calendrier de démonstration : semaine du 5 au 11 octobre 2026
    public static readonly string[] DayShort = { "Lun", "Mar", "Mer", "Jeu", "Ven", "Sam", "Dim" };
    public static readonly string[] DayLong = { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche" };
    public static readonly int[] DayNumber = { 5, 6, 7, 8, 9, 10, 11 };
    public static readonly string[] Times = { "09:00", "09:30", "10:00", "10:30", "11:00", "11:30" };

    public static string DateLabel(int day) => $"{DayLong[day]} {DayNumber[day]} octobre";
    public static bool IsAvailable(int day, int timeIndex) => day == 3 ? timeIndex != 0 : (day + timeIndex) % 4 != 0;

    public static void StartBooking(Practitioner p)
    {
        Selected = p;
        DayIndex = 3;
        Time = null;
    }

    public static Appointment Draft(string reminder, string motif) => new()
    {
        Practitioner = Selected!, DayIndex = DayIndex, StartTime = Time!, Reminder = reminder, Motif = motif,
    };

    public static void Book(string reminder, string motif)
    {
        LastBooked = Draft(reminder, motif);
        Upcoming.Add(LastBooked);
    }

    public static void CancelAppointment()
    {
        if (ToCancel is null) return;
        Upcoming.Remove(ToCancel);
        History.Insert(0, new HistoryItem
        {
            Header = $"{ToCancel.DateLabel} · {ToCancel.StartTime}",
            Status = "Annulé",
            Practitioner = ToCancel.Practitioner,
            Detail = "Annulé par vous · Aucun frais",
        });
        LastCancelled = ToCancel;
        ToCancel = null;
    }
}
