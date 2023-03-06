using MauiSaveUpDesktop.ViewModel;

namespace MauiSaveUpDesktop;

public partial class MainPage : ContentPage
{


    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }
}

