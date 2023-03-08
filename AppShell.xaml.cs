namespace MauiSaveUpDesktop;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(Views.Resultate), typeof(Views.Resultate));
    }
}
