namespace Statistics.MAUI.Blazor.App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new MainPage()) { Title = "Statistics.MAUI.Blazor.App" };
	}
}
