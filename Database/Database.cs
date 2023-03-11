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


        public List<Saves> Get()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(900);
                    var content = client.GetStringAsync(_dbPath).Result;
                    string lol = "";

                    return JsonConvert.DeserializeObject<List<Saves>>(content);
                }

            }
            catch { return null; }
        }

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
            catch { return null; }
        }
        public async Task<Saves> Add(Saves nahrung)
        {

            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(nahrung);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var content = await client.PostAsync(_dbPath, stringContent);
                content.EnsureSuccessStatusCode();
                return null;
            }

        }


    }
}
