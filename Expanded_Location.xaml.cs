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
using Weather.Models;

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

        }
        public async void weather(int Zip)
        {
            string zip = Zip.ToString();
            RootObject myWeather = await WeatherMap.GetWeather(zip, "imperial");
            string cityName = myWeather.name.ToString();
            City_Name.Text = cityName;

            double tempD = myWeather.main.temp;
            int tempI = Convert.ToInt32(tempD);
            string tempS = tempI.ToString();
            Current_Temp.Text = "Temp: " + tempS;

            tempD = myWeather.main.temp_max;
            tempI = Convert.ToInt32(tempD);
            tempS = tempI.ToString();
            Temp_High.Text = "Max: " + tempS;

            tempD = myWeather.main.temp_min;
            tempI = Convert.ToInt32(tempD);
            tempS = tempI.ToString();
            Temp_Low.Text = "Min: " + tempS;


            Description.Text = myWeather.weather[0].description;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllLocations));
        }
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            base.OnNavigatedTo(args);

            var parameters = (ExpandedParams)args.Parameter;

            weather(parameters.Zip);
        }
    }
}
