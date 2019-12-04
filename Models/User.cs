using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.DataAccess;

namespace Weather.Models
{
    public class User
    {
        public string Name { get; set; }
        public int DefaultZip { get; set; }
        public int MeasurmentSystem { get; set; }
        public int FontId { get; set; }
        public List<Location> Locations { get; set; }

        public User(String Name)
        {
            Locations = new List<Location>
            {
                new Location{City = "Searcy", State = "AR", Zip = 72143 },
                new Location{City = "Greenwood", State = "AR", Zip = 72936 },
                new Location{City = "Cedar Creek", State = "TX", Zip = 78612 }
            };
        }
    }
}
