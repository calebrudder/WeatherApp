using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.DataAccess;

namespace Weather.Models
{
    public class Location
    {
        public int Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public void AddToDb()
        {
            SqliteDb.AddLocation(this);
        }

        public void RemoveFromDb()
        {
            SqliteDb.DeleteLocation(Zip);
        }
    }
}
