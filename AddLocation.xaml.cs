using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Weather.Models;
using Weather.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public UserViewModel User { get; set; }
        public LocationViewModel Location { get; set; }
        public AddLocation()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            Location = new LocationViewModel(new Location());
            User = new UserViewModel(new User("Caleb"));
        }

        private void AddCity_Button_Click(object sender, RoutedEventArgs e)
        {
            User.Locations.Add(new LocationViewModel(
                new Models.Location { State = "AR", City = "Fort Smith", Zip = 72109 }));
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllLocations));
        }
    }
}
