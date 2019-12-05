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
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// public UserViewModel user;
    /// 
    public sealed partial class About : Page
    {
        public UserViewModel user;
        public About()
        {
            this.InitializeComponent();
            

        }
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("settings"))
            {
                user = new UserViewModel(new User(""));
                var settings = ApplicationData.Current.LocalSettings.Values["settings"] as ApplicationDataCompositeValue;
                user.FontId = Convert.ToInt32(settings["FontId"]);
            }
        }
    }
}
