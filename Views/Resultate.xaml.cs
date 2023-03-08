using MauiSaveUpDesktop.ViewModel;

namespace MauiSaveUpDesktop.Views;

public partial class Resultate : ContentPage
{
	public Resultate()
	{
		InitializeComponent();
		BindingContext = SharedData.Instance.Data;
	}
}