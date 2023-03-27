namespace MauiSaveUpDesktop.Models
{
    public class Saves
    {
        public int Id { get; set; }
        public string ArtikelName { get; set; }
        public float Betrag { get; set; }
        public string Kategorie { get; set; }
        public DateTime Datum { get; set; }
        //public Action<object, PropertyChangedEventArgs> PropertyChanged { get; internal set; }
    }
}