using System;
using Weather.DataAccess;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

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
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            weather();
        }
            public async void weather()
        {
            string zip, system, userName;
            string Greeting = "";

            if(ApplicationData.Current.LocalSettings.Values.ContainsKey("settings"))
            {
                var settings = ApplicationData.Current.LocalSettings.Values["settings"] as ApplicationDataCompositeValue;

                userName = settings["Name"].ToString();
                zip = settings["DefaultZip"].ToString();

                if ((bool)settings["Metric"] == true)
                {
                    system = "metric";
                }
                else
                {
                    system = "imperial";
                }
                
            }
            else
            {
                zip = "72149";
                userName = "";
                system = "imperial";
            }
            RootObject myWeather = await WeatherMap.GetWeather(zip, system);
            string icon = String.Format("http://openweathermap.org/img/wn/{0}.png", myWeather.weather[0].icon);
            result.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));


            // Get local time
            string time = DateTime.Now.ToString("h:mm tt");
            string hour = time[0].ToString();
            if(time[1] != ':')
            {
                hour += time[1].ToString();
            }
            string AmPm = time[5].ToString();
            AmPm += time[6].ToString();
            AmPm.Trim();

            int Hour = Int32.Parse(hour);

            // Determine if it is evening, afternoon, or morning
            if (Hour == 12 || Hour >= 1 && Hour <= 7 && AmPm == "PM")
            {
                Greeting = "Good afternoon " + userName;// + ", the weather is " + myWeather;
                greeting.Text = Greeting;
            }
            else if (Hour >= 8 && AmPm == "PM")
            {
                Greeting = "Good Evening " + userName;// + ", the weather is " + myWeather.weather;
                greeting.Text = Greeting;
            }
            else if(Hour >= 1 && Hour <= 11 && AmPm == "PM")
            {
                Greeting = "Good Morning " + userName;// + ", the weather is " + myWeather.weather;
                greeting.Text = Greeting;
            }

            string temp = myWeather.main.temp.ToString();
            temperature.Text = temp;

            // Determine the temperature
            string Temp = temp.Remove(2);
            int Temperature = Int32.Parse(Temp);

            if (Temperature <= 60 && Temperature >= 40 && system == "imperial")
            {
                Greeting += ", the temperature is cool";
                greeting.Text = Greeting;
            }
            else if (Temperature >= 61 && Temperature <= 80 && system == "imperial")
            {
                Greeting += ", the temperature is nice";
                greeting.Text = Greeting;
            }
            else if (Temperature <= 59 && Temperature >= 33 && system == "imperial")
            {
                Greeting += ", the temperature is cold";
                greeting.Text = Greeting;
            }
            else if (Temperature <= 32 && system == "imperial")
            {
                Greeting += ", the temperature is freezing";
                greeting.Text = Greeting;
            }

            temperature.Text = "It is currently " + myWeather.main.temp.ToString() + " degrees in " + myWeather.name.ToString();

            string Temp_low = myWeather.main.temp_min.ToString();
            low.Text = Temp_low;
            string Temp_high = myWeather.main.temp_max.ToString();
            high.Text = Temp_high;
            string Temp_wind = myWeather.wind.speed.ToString();
            wind.Text = Temp_wind;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddLocation));
        }
    }
}
