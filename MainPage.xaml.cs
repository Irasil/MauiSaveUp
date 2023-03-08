using MauiSaveUpDesktop.ViewModel;

namespace MauiSaveUpDesktop;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = SharedData.Instance.Data;
        MyPicker.SelectedIndex = 0;        
    }
}

