using System.ComponentModel;
using Weather.Models;

namespace Weather.ViewModels
{
    public class LocationViewModel 
    {

        private Location location;

        public void SaveNew()
        {
            location.AddToDb();
        }

        public void Delete()
        {
            location.RemoveFromDb();
        }
        public int Zip
        {
            get { return location.Zip; }
        }
        public string City
        {
            get { return location.City; }
        }
        public string State
        {
            get { return location.State; }
        }
    }

}
