using MauiSaveUpDesktop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSaveUpDesktop.Database
{
    public class Database
    {
        static string _dbPath = "https://saveup.kindfield-a2a62387.eastus.azurecontainerapps.io/SaveUp";


        public static List<Nahrung> Get()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(900);
                    var content = client.GetStringAsync(_dbPath).Result;

                    return JsonConvert.DeserializeObject<List<Nahrung>>(content);
                }

            }
            catch { return null; }
        }
    }
}
