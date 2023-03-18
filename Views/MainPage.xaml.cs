using MauiSaveUpDesktop.ViewModel;

namespace MauiSaveUpDesktop.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = SharedData.Instance.Data;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Betrag1.Text = null;
        Name.Text = string.Empty;
        MyPicker.SelectedIndex = 0;
        Loader.IsRunning = false;
    }
}

   

