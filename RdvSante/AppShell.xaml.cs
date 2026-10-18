using RdvSante.Pages;

namespace RdvSante;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Pages "empilées" (hors onglets)
        Routing.RegisterRoute("register", typeof(RegisterPage));
        Routing.RegisterRoute("practitioner", typeof(PractitionerPage));
        Routing.RegisterRoute("slots", typeof(SlotsPage));
        Routing.RegisterRoute("confirm", typeof(ConfirmPage));
        Routing.RegisterRoute("success", typeof(SuccessPage));
        Routing.RegisterRoute("cancel", typeof(CancelPage));
        Routing.RegisterRoute("cancelled", typeof(CancelledPage));
    }
}
