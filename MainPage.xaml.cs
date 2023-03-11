using MauiSaveUpDesktop.ViewModel;

namespace MauiSaveUpDesktop;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = SharedData.Instance.Data;
        //MyPicker.SelectedIndex = 0;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Betrag.Text = null;
        Name.Text = string.Empty;
        MyPicker.SelectedIndex = 0;
        Loader.IsRunning = false;
    }
}

   

