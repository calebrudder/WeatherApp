using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        }
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("json"))
            {
                //settings = user settings

                /*
                 * 1. create user obj
                 * 2. bind user information
                 */
            }
            else
            {
                //settings = default settings
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs args)
        {
            //save user information
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("json"))
            {
               //user.update
            }
            else
            {
                //user.save
            }
        }

    }
}
