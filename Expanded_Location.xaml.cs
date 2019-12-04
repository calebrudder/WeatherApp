using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Weather.DataAccess;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Expanded_Location : Page
    {

        public Expanded_Location()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            weather();
        }
        public async void weather()
        {
            int zip = 72149;
            RootObject myWeather = await WeatherMap.GetWeather(zip, "imperial");
            string cityName = myWeather.name.ToString();
            City_Name.Text = cityName;
            string temp = myWeather.main.temp.ToString();
            Current_Temp.Text = temp;
            string Temp_low = myWeather.main.temp_min.ToString();
            Temp_Low.Text = Temp_low;
            string Temp_high = myWeather.main.temp_max.ToString();
            Temp_High.Text = Temp_high;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllLocations));
        }
    }
}
