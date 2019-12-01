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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        AddLocation location = new AddLocation();
        public Home()
        {
            this.InitializeComponent();
            weather();
        }
        public async void weather()
        {
            //if() //if user exists, set to specifications, else do the following
            int zip = 72149;
            RootObject myWeather = await WeatherMap.GetWeather(zip, "imperial");
            string icon = String.Format("http://openweathermap.org/img/wn/{0}.png", myWeather.weather[0].icon);
            result.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));

            string temp = myWeather.main.temp.ToString();
            temperature.Text = temp;

            string Temp_low = myWeather.main.temp_min.ToString();
            low.Text = Temp_low;
            string Temp_high = myWeather.main.temp_max.ToString();
            high.Text = Temp_high;
            string Temp_wind = myWeather.wind.speed.ToString();
            wind.Text = Temp_wind;
            //string Temp_rain = myWeather.
            //rain.Text = Temp_rain;

            string cityName = myWeather.name;
            city.Text = cityName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddLocation));
        }
    }
}
