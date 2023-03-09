using MauiSaveUpDesktop.Models;
using MauiSaveUpDesktop.Database;



namespace MauiSaveUpDesktop.ViewModel
{
    public class SharedData
    {
        
        private static readonly Lazy<SharedData> lazy = new(() => new SharedData());

        public static SharedData Instance => lazy.Value;

        public MainPageViewModel Data { get; set; }

        private SharedData()
        {
            Data = new MainPageViewModel();
        }
    }

    public class MainPageViewModel : ViewModelBase
    {
        public Databases database = new Databases();
        public Saves _save = new();
        public Saves Save
        {
            get { return _save; }
            set
            {
                if (_save != value)
                {
                    _save = value;
                    OnPropertyChanged(nameof(Saves));
                }
            }
        }

        public string _selectedItem { get; set; }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }
        public List<Saves> _savesList { get; set; }
        public List<Saves> SaveList
        {
            get { return _savesList; }
            set
            {
                if (_savesList != value)
                {
                    _savesList = value;
                    OnPropertyChanged(nameof(Saves));
                }
            }
        }

        public string[] _kategorieErfassen { get; set; } = new string[]
       {
            "Nahrung",
            "Ausgang",
            "Elektronik"
       };

        public string[] KategorieErfassen
        {
            get { return _kategorieErfassen; }
            set
            {
                _kategorieErfassen = value;
                OnPropertyChanged(nameof(KategorieErfassen));
            }
        }
        public string[] _kategorieResultate { get; set; } = new string[]
      {
            "Alles",
            "Nahrung",
            "Ausgang",
            "Elektronik"
      };

        public string[] KategorieResultate
        {
            get { return _kategorieResultate; }
            set
            {
                _kategorieResultate = value;
                OnPropertyChanged(nameof(KategorieResultate));
            }
        }

        public Command _getCommand { get; set; }
        public Command _getToResultCommand { get; set; }
        public Command _backCommand { get; set; }
        public Command _addCommand { get; set; }


        public MainPageViewModel()
        {
            _getToResultCommand = new Command(GetToResult);
            _getCommand = new Command(Get);
            _backCommand = new Command(Back);
            _addCommand = new Command(Add);
        }

        public async void Add()
        {
            Save.Kategorie = SelectedItem;
            Save.Datum = DateTime.Now.Date;
            await database.Add(Save);
            
            GetToResult();
        }

        public void GetToResult()
        {
            Get();
            Shell.Current.GoToAsync("Resultate");
        }

        public void Get()
        {
            SaveList = database.Get();
        }

        public void Back()
        {            
            Shell.Current.GoToAsync("..");
        }

        public void ResetEntities()
        {
            Save.Betrag = 0;
            Save.ArtikelName = "";
        }


    }
}
