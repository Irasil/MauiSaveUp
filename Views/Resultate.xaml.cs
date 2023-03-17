using MauiSaveUpDesktop.ViewModel;
using System.Xml.Linq;

namespace MauiSaveUpDesktop.Views;

public partial class Resultate : ContentPage
{

    public Resultate()
    {
        InitializeComponent();
        BindingContext = SharedData.Instance.Data;
        MyPickerResult.SelectedIndex = 0;
    }



private void MyPicker_SelectedIndexChanged(object sender, EventArgs e)
{
    ((MainPageViewModel)BindingContext).PickerChanged();
}
}