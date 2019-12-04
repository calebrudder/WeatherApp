﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;
using Windows.Storage;

namespace Weather.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private User user;
        public ObservableCollection<LocationViewModel> Locations { get; set; }
       
        public UserViewModel(User user)
        {
            this.user = user;
            Locations = GetLocations();

        }

        public ObservableCollection<LocationViewModel> GetLocations()
        {
            ObservableCollection<LocationViewModel> LocationsList = new ObservableCollection<LocationViewModel>();

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("SavedLocations"))
            {
                //get all locations
                // Create ViewModels for each Location
                foreach (var location in user.Locations)
                {
                    var newLocation = new LocationViewModel(location);
                    newLocation.PropertyChanged += OnPropertyChanged;
                    LocationsList.Add(newLocation);
                }

            }
            else
            {
                Location location = new Location();
                location.City = "Searcy";
                location.State = "AR";
                location.Zip = 72143;
                LocationsList = new ObservableCollection<LocationViewModel>
                {
                     new LocationViewModel(location)
                };
            }
            return LocationsList;
        }
        public void SaveLocations(ObservableCollection<LocationViewModel> LocationsList)
        {
            var SavedLocations = new ApplicationDataCompositeValue();
            string json = JsonConvert.SerializeObject(LocationsList);
            SavedLocations["SavedLocationsList"] = json;
            ApplicationData.Current.LocalSettings.Values["SavedLocations"] = SavedLocations;
        }

        public void Save()
        {
            var settings = new ApplicationDataCompositeValue();
            settings["Name"] = user.Name;
            settings["City"] = user.City;
            settings["State"] = user.State;
            settings["DefaultZip"] = user.DefaultZip;
            settings["Imperial"] = user.Imperial;
            settings["Metric"] = user.Metric;
            settings["FontId"] = user.FontId;
            ApplicationData.Current.LocalSettings.Values["settings"] = settings;
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

        public string Name
        {
            get { return user.Name; }
            set
            {
                user.Name = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public string DefaultZip
        {
            get { return user.DefaultZip; }
            set
            {
                user.DefaultZip = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("DefaultZip"));
            }
        }
        public bool Imperial
        {
            get { return user.Imperial; }
            set
            {
                user.Imperial = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Imperial"));
            }
        }
        public bool Metric
        {
            get { return user.Metric; }
            set
            {
                user.Metric = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Metric"));
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

        public string City
        {
            get { return user.City; }
            set
            {
                user.City = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("City"));
            }
        }
        public string State
        {
            get { return user.State; }
            set
            {
                user.State = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("State"));
            }
        }
    }
}
