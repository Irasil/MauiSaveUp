using MauiSaveUpDesktop.ViewModel;
using System.Xml.Linq;

namespace MauiSaveUpDesktop.Views;

public partial class Resultate : ContentPage
{
	public Resultate()
	{
		InitializeComponent();
		BindingContext = SharedData.Instance.Data;
	}
    protected override async void OnAppearing()
    {
        ((MainPageViewModel)BindingContext).Get();
        base.OnAppearing();
        ListView.ItemsSource = ((MainPageViewModel)BindingContext).SaveList; 
    }
}