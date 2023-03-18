using MauiSaveUpDesktop.Models;
using MauiSaveUpDesktop.Database;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading;

namespace MauiSaveUpDesktop.ViewModel
{

    /// <summary>
    /// Klasse, um die Daten zwischen den Views zu teilen
    /// </summary>
    public class SharedData
    {

        private static readonly Lazy<SharedData> lazy = new(() => new SharedData());

        public static SharedData Instance => lazy.Value;

        public MainPageViewModel Data { get; set; }
        //public Database data;
        private SharedData()
        {
            Data = new MainPageViewModel();
        }
    }

    /// <summary>
    /// ViewModel für die MainPage und ResultatePage
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {

        #region Properties
        public Databases database = new Databases();
        ObservableCollection<Saves> SaveListTemp = new ObservableCollection<Saves>();


        private bool _loading;
        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        private Saves _save = new();
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

        private string _selectedItemErfassen;
        public string SelectedItemErfassen
        {
            get { return _selectedItemErfassen; }
            set
            {
                if (_selectedItemErfassen != value)
                {
                    _selectedItemErfassen = value;
                    OnPropertyChanged(nameof(SelectedItemErfassen));
                }
            }
        }
        private string _selectedItemResultate;
        public string SelectedItemResultate
        {
            get { return _selectedItemResultate; }
            set
            {
                if (_selectedItemResultate != value)
                {
                    _selectedItemResultate = value;
                    OnPropertyChanged(nameof(SelectedItemResultate));
                }
            }
        }
        private ObservableCollection<Saves> _savesList;
        public ObservableCollection<Saves> SaveList
        {
            get { return _savesList; }
            set
            {
                if (_savesList != value)
                {
                    _savesList = value;
                    OnPropertyChanged(nameof(SaveList));
                }
            }
        }
        private ObservableCollection<Saves> _savesListFix;
        public ObservableCollection<Saves> SaveListFix
        {
            get { return _savesListFix; }
            set
            {
                if (_savesListFix != value)
                {
                    _savesListFix = value;
                    OnPropertyChanged(nameof(SaveListFix));
                }
            }
        }

        public string[] _kategorieErfassen = new string[] { "Nahrung","Ausgang","Elektronik"};
        public string[] KategorieErfassen
        {
            get { return _kategorieErfassen; }
            set
            {
                _kategorieErfassen = value;
                OnPropertyChanged(nameof(KategorieErfassen));
            }
        }
        public string[] _kategorieResultate { get; set; } = new string[] { "Alles", "Nahrung", "Ausgang", "Elektronik" };
        public string[] KategorieResultate
        {
            get
            {
                PickerChanged();
                return _kategorieResultate;
            }
            set
            {
                _kategorieResultate = value;
                OnPropertyChanged(nameof(KategorieResultate));
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged(nameof(SelectedIndex));
                    // Do something with the new selection
                }
            }
        }
        private double _betragTotal;
        public double BetragTotal
        {
            get { return _betragTotal; }
            set
            {
                if (_betragTotal != value)
                {
                    _betragTotal = value;
                    OnPropertyChanged(nameof(BetragTotal));
                }
            }
        }

        private Color _bgAlles = Color.FromRgba("#0553A0");
        public Color BGAlles
        {
            get { return _bgAlles; }
            set
            {
                if (_bgAlles != value)
                {
                    _bgAlles = value;
                    OnPropertyChanged(nameof(BGAlles));
                }
            }
        }

        private Color _bgMonat;
        public Color BGMonat
        {
            get { return _bgMonat; }
            set
            {
                if (_bgMonat != value)
                {
                    _bgMonat = value;
                    OnPropertyChanged(nameof(BGMonat));
                }
            }
        }
        private Color _bgTag;
        public Color BGTag
        {
            get { return _bgTag; }
            set
            {
                if (_bgTag != value)
                {
                    _bgTag = value;
                    OnPropertyChanged(nameof(BGTag));
                }
            }
        }

        public Command _getCommand { get; set; }
        public Command _getToResultCommand { get; set; }
        public Command _backCommand { get; set; }
        public Command _addCommand { get; set; }
        public Command _allesCommand { get; set; }
        public Command _monatCommand { get; set; }
        public Command _tagCommand { get; set; }
        public Command _deleteCommand { get; set; }
        #endregion

        //private IDataPicker _data;
        /// <summary>
        /// Konstruktor
        /// </summary>
        public MainPageViewModel()
        {
            //_data = data;

            _getToResultCommand = new Command(GetToResult);
            _getCommand = new Command(Get);
            _backCommand = new Command(Back);
            _addCommand = new Command(Add);
            _tagCommand = new Command(GetTag);
            _monatCommand = new Command(GetMonat);
            _allesCommand = new Command(GetAlles);
        }

        /// <summary>
        /// Methode, um den geswipten Eintrag zu löschen
        /// </summary>
        public ICommand DeleteCommand => new Command<Saves>((saveItem) =>
        {
            // Delete the item from the SaveList...
            SaveListFix.Remove(saveItem);
            Delete(saveItem);

        });

        /// <summary>
        /// Methode, um einen neuen Eintrag zu erfassen
        /// </summary>
        public async void Add()
        {
            if (Save.Betrag > 0 && !String.IsNullOrWhiteSpace(Save.ArtikelName))
            {
                Save.Kategorie = SelectedItemErfassen;
                Save.Datum = DateTime.Now.Date;
                await database.Add(Save);
                GetToResult();
            }
            else if (Save.Betrag <= 0 && !String.IsNullOrWhiteSpace(Save.ArtikelName))
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte geben Sie eine positive Zahl ein", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte füllen Sie alle Felder aus", "OK");
            }
        }

        /// <summary>
        /// Methode, um auf der ResultatePage alle Einträge anzuzeigen
        /// </summary>
        public async void GetToResult()
        {
            Loading = true;
            Get();
            await Shell.Current.GoToAsync("Resultate");
            Loading = false;
        }

        /// <summary>
        /// Methode, um die ListView zu aktualisieren
        /// </summary>
        public void Get()
        {
            SaveListTemp = new ObservableCollection<Saves>();
            List<Saves> saves = database.Get();
            SaveListFix = new ObservableCollection<Saves>(saves);
            SaveList = SaveListFix;
            GetTotal();
        }

        /// <summary>
        /// Zurück zu der MainPage
        /// </summary>
        public void Back()
        {
            //Shell.Current.GoToAsync("..");
            Shell.Current.GoToAsync("MainPage");
        }

        /// <summary>
        /// Methode, um die Daten der entsprechenden Kategorie anzuzeigen
        /// </summary>
        public void PickerChanged()
        {            
            SaveListTemp = new ObservableCollection<Saves>();
            switch (_selectedItemResultate)
            {
                case "Alles":
                    AllesButtonFarbe();
                    Get();
                    break;
                case "Nahrung":
                    AllesButtonFarbe();
                    foreach (Saves save in SaveListFix)
                    {
                        if (save.Kategorie == "Nahrung")
                        {
                            SaveListTemp.Add(save);
                        }
                    }
                    SaveList = SaveListTemp;
                    GetTotal();
                    break;
                case "Ausgang":
                    AllesButtonFarbe();
                    foreach (Saves save in SaveListFix)
                    {
                        if (save.Kategorie == "Ausgang")
                        {
                            SaveListTemp.Add(save);
                        }
                    }
                    SaveList = SaveListTemp;
                    GetTotal();
                    break;
                case "Elektronik":
                    AllesButtonFarbe();
                    foreach (Saves save in SaveListFix)
                    {
                        if (save.Kategorie == "Elektronik")
                        {
                            SaveListTemp.Add(save);
                        }
                    }
                    SaveList = SaveListTemp;
                    GetTotal();
                    break;
            }
        }
        private void AllesButtonFarbe() 
        {
            BGTag = Color.FromArgb("#3a97f2");
            BGMonat = Color.FromArgb("#3a97f2");
            BGAlles = Color.FromRgba("#0553A0");
        }

        /// <summary>
        /// Methode um das Tatal auszurechnen
        /// </summary>
        public void GetTotal()
        {
            BetragTotal = 0;
            foreach (var save in SaveList)
            {
                BetragTotal += save.Betrag;
            }
            double roundedValue = (double)Math.Round(BetragTotal, 2, MidpointRounding.AwayFromZero);

            BetragTotal = (double)Math.Round(roundedValue * 20) / 20;
        }

        /// <summary>
        /// Alle Einträge des momentanen Tages anzeigen
        /// </summary>
        public async void GetTag()
        {
            Loading = true;
            await Task.Delay(100);
            PickerChanged();
            SaveListTemp = new ObservableCollection<Saves>();


            foreach (var save in SaveList)
            {
                if (save.Datum.Day == DateTime.Now.Date.Day)
                {
                    SaveListTemp.Add(save);
                }
            }

            SaveList = SaveListTemp;
            GetTotal();
            BGAlles = Color.FromArgb("#3a97f2");
            BGMonat = Color.FromArgb("#3a97f2");
            BGTag = Color.FromRgba("#0553A0");
            Loading = false;
        }

        /// <summary>
        /// Alle Einträge des momentanen Monats anzeigen
        /// </summary>
        public async void GetMonat()
        {
            Loading = true;
            await Task.Delay(100);
            PickerChanged();
            SaveListTemp = new ObservableCollection<Saves>();


            foreach (var save in SaveList)
            {
                if (save.Datum.Month == DateTime.Now.Month)
                {
                    SaveListTemp.Add(save);
                }
            }
            SaveList = SaveListTemp;
            GetTotal();
            BGTag = Color.FromArgb("#3a97f2");
            BGAlles = Color.FromArgb("#3a97f2");
            BGMonat = Color.FromRgba("#0553A0");
            Loading = false;
        }

        /// <summary>
        /// Alle Einträge Alle einträge der bestimmten Kategorie Anzeigen
        /// </summary>
        public async void GetAlles()
        {
            Loading = true;
            await Task.Delay(100);
            PickerChanged();
            SaveListTemp = new ObservableCollection<Saves>();


            foreach (var save in SaveList)
            {
                if (save.Kategorie == _selectedItemResultate || _selectedItemResultate == "Alles")
                {
                    SaveListTemp.Add(save);
                }
            }
            SaveList = SaveListTemp;
            GetTotal();
            AllesButtonFarbe();
            Loading = false;
        }

        /// <summary>
        /// Eintrag löschen
        /// </summary>
        /// <param name="saves">Das zulöschende Objekt</param>
        public async void Delete(Saves saves)
        {
            Loading = true;
            await database.Delete(saves);
            await App.Current.MainPage.DisplayAlert("Erfolg", $" {saves.ArtikelName} wurde erfolgreich gelöscht", "OK");
            SaveList.Remove(saves);
            Loading = false;
        }
    }
}
