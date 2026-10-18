using System.Text.RegularExpressions;

namespace RdvSante.Services;

public static class Nav
{
    public static Task Back() => Shell.Current.GoToAsync("..");
    public static Task Go(string route) => Shell.Current.GoToAsync(route);
    public static Task ToLogin() => Shell.Current.GoToAsync("//login");
    public static Task ToHome() => Shell.Current.GoToAsync("//main/home/search");
    public static Task ToAppointments() => Shell.Current.GoToAsync("//main/appointments/myappointments");
}

public static class Ui
{
    public static Style? Get(string key) =>
        Application.Current!.Resources.TryGetValue(key, out var value) ? value as Style : null;

    /// <summary>Affiche (ou masque) le message d'erreur / de validation d'un champ.</summary>
    public static void SetState(Border field, Label message, string? error, string? ok = null)
    {
        field.Style = Get(error is null ? "Field" : "FieldError");
        if (error is not null)
        {
            message.Style = Get("ErrorText");
            message.Text = error;
            message.IsVisible = true;
        }
        else if (ok is not null)
        {
            message.Style = Get("SuccessText");
            message.Text = ok;
            message.IsVisible = true;
        }
        else
        {
            message.IsVisible = false;
        }
    }

    public static bool IsEmail(string? s) =>
        !string.IsNullOrWhiteSpace(s) && Regex.IsMatch(s.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$");

    public static bool IsCameroonPhone(string? s) =>
        !string.IsNullOrWhiteSpace(s) && Regex.IsMatch(s.Replace(" ", ""), @"^(\+?237)?6\d{8}$");
}
