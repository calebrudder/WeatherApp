using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Weather.Models;
using Weather.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public UserViewModel User { get; set; }
        public LocationViewModel Location { get; set; }
        public AllLocations()
        {
            this.InitializeComponent();

            Location = new LocationViewModel(new Location());
            User = new UserViewModel(new User("Caleb"));
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void Add_Location_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddLocation));
        }

        private void Locations_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void Locations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            LocationViewModel clickedLocation = (sender as ListView).SelectedItem as LocationViewModel;
            var parameters = new ExpandedParams();
            parameters.Zip = clickedLocation.Zip;

            this.Frame.Navigate(typeof(Expanded_Location), parameters);
        }
    }
}
