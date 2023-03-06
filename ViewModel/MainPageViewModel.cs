using MauiSaveUpDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSaveUpDesktop.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPage mainPage;

        public Nahrung nahrung = new Nahrung();
        public List<Nahrung> nahrungList { get; set; } = new List<Nahrung>();



        public List<Nahrung> nah
        {
            get { return nahrungList; }
            set
            {
                nahrungList = value;
                OnPropertyChanged(nameof(nah));
            }
        }


        public Command _getCommand { get; set; }


        public MainPageViewModel()
        {
            _getCommand = new Command(async () => Get());
        }

        public void Get()
        {
            nah = Database.Database.Get();

        }
    }
}
