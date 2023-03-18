using MauiSaveUpDesktop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;


namespace MauiSaveUpDesktop.Database
{
    public class Databases
    {
        public string _dbPath = "https://saveupapi.azurewebsites.net/SaveUp";

        /// <summary>
        /// Alle Daten von der Datenbank
        /// </summary>
        /// <returns></returns>
        public List<Saves> Get()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(900);
                    var content = client.GetStringAsync(_dbPath).Result;
                    return JsonConvert.DeserializeObject<List<Saves>>(content);
                }

            }
            catch { App.Current.MainPage.DisplayAlert("Fehler", "Es konnte keine Verbindung zum Server hergestellt werden", "OK"); return null; }
            
        }

        /// <summary>
        /// Alle Daten von der Datenbank nach Kategorie
        /// </summary>
        /// <param name="kategorie"></param>
        /// <returns></returns>
        public List<Saves> GetByKategorie(string kategorie)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(900);
                    var content = client.GetStringAsync(_dbPath).Result;
                    var list = JsonConvert.DeserializeObject<List<Saves>>(content);
                    

                    List<Saves> saves = new();
                    foreach (var item in list)
                    {
                        if (item.Kategorie == kategorie)
                        {
                            saves.Add(item);
                        }
                    }
                    return saves;
                }

            }
            catch { App.Current.MainPage.DisplayAlert("Fehler", "Es konnte keine Verbindung zum Server hergestellt werden", "OK"); return null; }
        }
        
        /// <summary>
        /// Eintrag in der Datenbank über die API speichern
        /// </summary>
        /// <param name="eintrag">Neuer Eintrag</param>
        /// <returns></returns>
        public async Task<Saves> Add(Saves eintrag)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var jsonContent = JsonConvert.SerializeObject(eintrag);
                    var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var content = await client.PostAsync(_dbPath, stringContent);
                    content.EnsureSuccessStatusCode();
                    return null;
                }
            }
            catch { await App.Current.MainPage.DisplayAlert("Fehler", "Es konnte keine Verbindung zum Server hergestellt werden", "OK"); return null; }
        }
        
        /// <summary>
        /// Eintrag von der Datenbank über die API löschen
        /// </summary>
        /// <param name="eintrag"></param>
        /// <returns></returns>
        public async Task<Saves> Delete(Saves eintrag)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var jsonContent = JsonConvert.SerializeObject(eintrag);
                    var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var respons = await client.DeleteAsync($"{_dbPath}/{eintrag.Id}");
                    respons.EnsureSuccessStatusCode();
                    return null;
                }
            }catch { await App.Current.MainPage.DisplayAlert("Fehler", "Es konnte keine Verbindung zum Server hergestellt werden", "OK"); return null; }
        }

    }
}
