using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private User user;
        public ObservableCollection<LocationViewModel> Locations { get; set; }
        public string Name
        {
            get { return user.Name; }
            set
            {
                user.Name = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public int DefaultZip
        {
            get { return user.DefaultZip; }
            set
            {
                user.DefaultZip = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("DefaultZip"));
            }
        }
        public int MeasurmentSystem
        {
            get { return user.MeasurmentSystem; }
            set
            {
                user.MeasurmentSystem = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("MeasurmentSystem"));
            }
        }
        public int FontId
        {
            get { return user.FontId; }
            set
            {
                user.FontId = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("FontId"));
            }
        }
        public UserViewModel(User user)
        {
            this.user = user;

            Locations = new ObservableCollection<LocationViewModel>();

            // Create ViewModels for each Movie
            foreach (var location in user.Locations)
            {
                var newLocation = new LocationViewModel(location);
                newLocation.PropertyChanged += OnPropertyChanged;
                Locations.Add(newLocation);
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(user);

        }
        public void Update()
        {
           string json = JsonConvert.SerializeObject(user);
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // User name or LocationViewModel changed, so let UI know
            PropertyChanged?.Invoke(sender, e);
        }
    }
}
