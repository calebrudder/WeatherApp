﻿using System.ComponentModel;
using Weather.Models;

namespace Weather.ViewModels
{
    public class LocationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Location location;

        public LocationViewModel(Location location)
        {
            this.location = location;
        }

        public int Zip
        {
            get { return location.Zip; }
            set
            {
                location.Zip = value;
                OnPropertyChanged("Zip");
            }
        }
        public string City {
            get { return location.City; }
            set
            {
                location.City = value;
                OnPropertyChanged("City");
            }
        }
        public string State
        {
            get { return location.State; }
            set
            {
                location.State = value;
                OnPropertyChanged("State");
            }
        }

        private void OnPropertyChanged(string property)
        {
            // Notify any controls bound to the ViewModel that the property changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }

}
