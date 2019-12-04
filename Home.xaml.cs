using System;
using Weather.DataAccess;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Weather
{
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
            int zip;

            if(ApplicationData.Current.LocalSettings.Values.ContainsKey("json"))
            {
                string json = ApplicationData.Current.LocalSettings.Values["json"] as string;
                // UserObject = JsonConvert.DeserializeObject<>(json);
                zip = 1;//UserObject.Zip;
                //message.Text = "Welcome, " + UserObject.Name + ", the weather looks "
                //temperature.Text = "It is currently " + UserObject.Temp + " degrees in " + UserObject.
            }
            else
            {
                zip = 72149;
            }
            RootObject myWeather = await WeatherMap.GetWeather(zip, "imperial");
            string icon = String.Format("http://openweathermap.org/img/wn/{0}.png", myWeather.weather[0].icon);
            result.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));

            string temp = myWeather.main.temp.ToString();
            temperature.Text = temp;

            temperature.Text = "It is currently " + myWeather.main.temp.ToString() + " degrees in " + myWeather.name.ToString();

            string Temp_low = myWeather.main.temp_min.ToString();
            low.Text = Temp_low;
            string Temp_high = myWeather.main.temp_max.ToString();
            high.Text = Temp_high;
            string Temp_wind = myWeather.wind.speed.ToString();
            wind.Text = Temp_wind;

            string cityName = myWeather.name;
            city.Text = cityName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddLocation));
        }
    }
}
