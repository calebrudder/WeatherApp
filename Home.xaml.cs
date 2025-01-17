﻿using System;
using Weather.DataAccess;
using Weather.Models;
using Weather.ViewModels;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Weather
{
    public sealed partial class Home : Page
    {
        AddLocation location = new AddLocation();
        public UserViewModel user;
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
            string zip, system, userName, city, Greeting;

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("settings"))
            {
                user = new UserViewModel(new User(""));
                var settings = ApplicationData.Current.LocalSettings.Values["settings"] as ApplicationDataCompositeValue;

                user.Name = (string)settings["Name"];
                user.City = (string)settings["City"];
                user.State = (string)settings["State"];
                user.DefaultZip = (string)settings["DefaultZip"];
                user.FontId = Convert.ToInt32(settings["FontId"]);
                user.Imperial = (bool)settings["Imperial"];
                user.Metric = (bool)settings["Metric"];

                userName = settings["Name"].ToString();
                city = settings["City"].ToString();
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
                city = "Searcy";
                userName = "";
                system = "imperial";
            }
            RootObject myWeather = await WeatherMap.GetWeather(zip, system);
            string icon = String.Format("http://openweathermap.org/img/wn/{0}.png", myWeather.weather[0].icon);
            result.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));

            // Get local time
            string time = DateTime.Now.ToString("h:mm tt");
            string hour = time[0].ToString();
            if (time[1] != ':')
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
                Greeting = "Good afternoon " + userName + ", looks like " + myWeather.weather[0].description;
                greeting.Text = Greeting;
            }
            else if (Hour >= 8 && AmPm == "PM")
            {
                Greeting = "Good Evening " + userName + ", looks like " + myWeather.weather[0].description;
                greeting.Text = Greeting;
            }
            else if (Hour >= 1 && Hour <= 11 && AmPm == "PM")
            {
                Greeting = "Good Morning " + userName + ", looks like " + myWeather.weather[0].description;
                greeting.Text = Greeting;
            }

            temperature.Text = "It is currently " + myWeather.main.temp.ToString() + " degrees in " + city;

            string Temp_low = myWeather.main.temp_min.ToString();
            low.Text = Temp_low;
            string Temp_high = myWeather.main.temp_max.ToString();
            high.Text = Temp_high;
            string Temp_wind = myWeather.wind.speed.ToString();
            wind.Text = Temp_wind;
            string humidity = myWeather.main.humidity.ToString();
            rain.Text = humidity + "%";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddLocation));
        }
    }
}
