using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Weather.DataAccess;
using Weather.Models;
using Weather.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weather
{

    public sealed partial class AddLocation : Page
    {
        public ObservableCollection<LocationViewModel> LocationsList { get; set; }
        public UserViewModel User { get; set; }
        public LocationViewModel Location { get; set; }
        public AddLocation()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            Location = new LocationViewModel(new Location());
            User = new UserViewModel(new User(" "));

    }

        private void AddCity_Button_Click(object sender, RoutedEventArgs e)
        {
            string addCity = City.Text;
            string addState = State.Text;
            string addZip = Zip.Text;
            string system = "imperial";

            if (DefaultCity_Checkbox.IsChecked == true)
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("settings"))
                {
                    //settings = user settings
                    var settings = ApplicationData.Current.LocalSettings.Values["settings"] as ApplicationDataCompositeValue;

                    User.Name = (string)settings["Name"];
                    User.City = addCity;
                    User.State = addState;
                    User.DefaultZip = addZip;
                    User.Imperial = (bool)settings["Imperial"];
                    User.Metric = (bool)settings["Metric"];
                    User.Save();

                }
                else
                {
                    //settings = default settings
                    User.City = addCity;
                    User.State = addState;
                    User.DefaultZip = addZip;
                    User.FontId = 16;
                    User.Name = "";
                    User.Imperial = true;
                    User.Metric = false;
                    User.Save();
                }
            }
           

            LocationsList.Add(new LocationViewModel(
                new Models.Location { State = addState, City = addCity, Zip = addZip }));


            User.SaveLocations(LocationsList);

            this.Frame.Navigate(typeof(AllLocations));
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllLocations));
        }
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            base.OnNavigatedTo(args);

            var parameters = (ObservableCollection<LocationViewModel>)args.Parameter;

            LocationsList = parameters;
        }
    }
}
