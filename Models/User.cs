using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.DataAccess;
using Windows.Storage;

namespace Weather.Models
{
    public class User
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string DefaultZip { get; set; }
        public bool Imperial { get; set; }
        public bool Metric { get; set; }
        public int FontId { get; set; }
        public List<Location> Locations { get; set; }

        public User(String Name)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("SavedLocations"))
            {
                //get all locations
                var SavedLocations = ApplicationData.Current.LocalSettings.Values["SavedLocations"] as ApplicationDataCompositeValue;
                //set locations = to all of these locations
                string saved = (string)SavedLocations["SavedLocationsList"];
                Locations = JsonConvert.DeserializeObject<List<Location>>(saved);

            }
            else
            {
                Locations = new List<Location>
                {
                     new Location{City = "Searcy", State = "AR", Zip = 72143 }
                };
            }
        }
    }
}
