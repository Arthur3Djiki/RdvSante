namespace RdvSante;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());
#if WINDOWS
        // Fenêtre au format téléphone pour retrouver les proportions des maquettes.
        window.Width = 430;
        window.Height = 900;
#endif
        return window;
    }
}
