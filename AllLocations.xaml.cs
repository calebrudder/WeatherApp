using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Weather.DataAccess;
using Weather.Models;
using Weather.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Weather
{
    public sealed partial class AllLocations : Page
    {
        public ObservableCollection<LocationViewModel> LocationsList { get; set; }
        public UserViewModel user = new UserViewModel(new User(" "));
        public LocationViewModel rightClicked;
        public AllLocations()
        {
            this.InitializeComponent();
           
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void Add_Location_Click(object sender, RoutedEventArgs e)
        {
            var parameters = LocationsList;
            this.Frame.Navigate(typeof(AddLocation), parameters);
        }

        private void Locations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            LocationViewModel clickedLocation = (sender as ListView).SelectedItem as LocationViewModel;
            if (clickedLocation != null)
            {
                var parameters = new ExpandedParams();
                parameters.Zip = clickedLocation.Zip;

                this.Frame.Navigate(typeof(Expanded_Location), parameters);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {

            LocationsList = user.Locations;
            user.SaveLocations(LocationsList);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs args)
         {
            user.SaveLocations(LocationsList);
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var parameters = LocationsList;
            this.Frame.Navigate(typeof(AddLocation), parameters);
        }

        private void Default_Click(object sender, RoutedEventArgs e)
        {
            LocationViewModel newDefault = Locations.SelectedItem as LocationViewModel;
            string newState = newDefault.State;
            string newzip = newDefault.Zip;
            string newCity = newDefault.City;

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("settings"))
            {
                //settings = user settings
                var settings = ApplicationData.Current.LocalSettings.Values["settings"] as ApplicationDataCompositeValue;

                user.Name = (string)settings["Name"];
                user.City = newCity;
                user.State = newState;
                user.DefaultZip = newzip;
                user.Imperial = (bool)settings["Imperial"];
                user.Metric = (bool)settings["Metric"];
                user.Save();

            }
            else
            {
                //settings = default settings
                user.City = newCity;
                user.State = newState;
                user.DefaultZip = newzip;
                user.FontId = 1;
                user.Name = "";
                user.Imperial = true;
                user.Metric = false;
                user.Save();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            foreach (var location in LocationsList)
            {
                if (location.Zip == rightClicked.Zip)
                {
                    MessageDialog confirm = new MessageDialog("Are you sure you want to delete the location " + location.City + ", " + location.State + "?");

                    confirm.Commands.Add(new UICommand("Yes")
                    {
                        Id = 0
                    });
                    confirm.Commands.Add(new UICommand("No")
                    {
                        Id = 1
                    });
                    confirm.DefaultCommandIndex = 0;
                    confirm.CancelCommandIndex = 1;
                    var result = confirm.ShowAsync();
                    if ((int)result.Id == 0)
                    {
                        LocationsList.Remove(LocationsList.Where(i => i.Zip == deleteBtn.Name).Single());
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            user.SaveLocations(LocationsList);
        }

        private void Locations_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            rightClicked = (sender as ListView).SelectedItem as LocationViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button deleteBtn = e.OriginalSource as Button;
           
            foreach (var location in LocationsList)
            {
                if (location.Zip ==  deleteBtn.Name)
                {
                    MessageDialog confirm = new MessageDialog("Are you sure you want to delete the location " + location.City + ", " + location.State + "?");

                    confirm.Commands.Add(new UICommand("Yes")
                    {
                        Id = 0
                    });
                    confirm.Commands.Add(new UICommand("No")
                    {
                        Id = 1
                    });
                    confirm.DefaultCommandIndex = 0;
                    confirm.CancelCommandIndex = 1;
                    var result = confirm.ShowAsync();
                    if ((int)result.Id == 0)
                    {
                        LocationsList.Remove(LocationsList.Where(i => i.Zip == deleteBtn.Name).Single());
                        break;
                    }
                    else
                    {
                        break;
                    }


                }
            }
            user.SaveLocations(LocationsList);
        }
        
    }
}
