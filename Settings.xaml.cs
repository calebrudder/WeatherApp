using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weather
{
    public sealed partial class Settings : Page
    {

        public UserViewModel user;
        public Settings()
        {
            this.InitializeComponent();
            user = new UserViewModel(new User(""));
        }
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {        
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("settings"))
            {
                //settings = user settings
                var settings = ApplicationData.Current.LocalSettings.Values["settings"] as ApplicationDataCompositeValue;

                user.Name = (string)settings["Name"];
                user.City = (string)settings["City"];
                user.State = (string)settings["State"];
                user.DefaultZip = (string)settings["DefaultZip"];
                user.FontId = Convert.ToInt32(settings["FontId"]);
                user.FontSize = settings["FontId"].ToString();
                user.Imperial = (bool)settings["Imperial"];
                user.Metric = (bool)settings["Metric"];
                user.FontSize = settings["FontId"].ToString();
            }
            else
            {
                //settings = default settings
                
                user.DefaultZip = "72149";
                user.City = "Searcy";
                user.State = "AR";
                user.FontId = 16;
                user.FontSize = "16";
                user.Name = "";
                user.Imperial = true;
                user.Metric = false;
            }
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs args)
        {
            //save user information
            user.Name = Name_Textbox.Text;
            user.City = City.Text;
            user.State = State.SelectedValue.ToString();
            user.FontId = Convert.ToInt32(Current_Font_Settings.SelectedValue);
            user.FontSize = Current_Font_Settings.Text;
            user.DefaultZip = Zip.Text;
            user.Imperial = (bool)Imperial.IsChecked;
            user.Metric = (bool)Metric.IsChecked;
            user.Save();
        }

    }
}
