using MauiSaveUpDesktop.Models;


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

        public string[] _kategorie { get; set; } = new string[]
       {
            "Alles",
            "Nahrung",
            "Ausgang",
            "Elektronik"
       };

        public string[] Kategorie
        {
            get { return _kategorie; }
            set
            {
                _kategorie = value;
                OnPropertyChanged(nameof(Kategorie));
            }
        }

        public Command _getCommand { get; set; }
        public Command _backCommand { get; set; }
        public Command _addCommand { get; set; }


        public MainPageViewModel()
        {
            _getCommand = new Command(async () => Get());
            _backCommand = new Command(async () => Back());
            _addCommand = new Command(async () => Add());
        }

        public async void Add()
        {
            Save.Kategorie = SelectedItem;
            Save.Datum = DateTime.Now.Date;
            Database.Database.Add(Save);
            Task.Delay(2000);
            SaveList = Database.Database.Get();
            Shell.Current.GoToAsync("Resultate");
        }

        public void Get()
        {
            SaveList = Database.Database.Get();
            Shell.Current.GoToAsync("Resultate");
        }

        public void Back()
        {
            Shell.Current.GoToAsync("MainPage");
        }


    }
}
