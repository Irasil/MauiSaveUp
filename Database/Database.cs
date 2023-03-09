using MauiSaveUpDesktop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiSaveUpDesktop.Database
{
    public class Database
    {
        static string _dbPath = "https://saveup-app-20230308224317.whitebay-b0072808.eastus.azurecontainerapps.io/SaveUp";


        public static List<Saves> Get()
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
            catch { return null; }
        }
        public static async void Add(Saves nahrung)
        {
            try
            {
                using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(900);
                var content = await client.PostAsJsonAsync(_dbPath, nahrung);
                    
            }
            }
            catch (Exception ex) { }
        }
    }
}
