using MauiSaveUpDesktop.ViewModel;
using System.Xml.Linq;

namespace MauiSaveUpDesktop.Views;

public partial class Resultate : ContentPage
{
    
	public Resultate()
	{
		InitializeComponent();
		BindingContext = SharedData.Instance.Data;
        MyPicker.SelectedIndex = 0; 
	}
    //protected override async void OnAppearing()
    //{       
    //    base.OnAppearing();
    //    ((MainPageViewModel)BindingContext).PickerChanged();
    //    ListView.ItemsSource = ((MainPageViewModel)BindingContext).SaveList; 
    //}

    private void MyPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        ((MainPageViewModel)BindingContext).PickerChanged();
    }
}