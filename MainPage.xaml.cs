using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Weather.DataAccess;
using Weather.Models;
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


namespace Weather
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void nvTopLevelNav_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in nvTopLevelNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Home_Page")
                {
                    nvTopLevelNav.SelectedItem = item;
                    break;
                }
            }
            contentFrame.Navigate(typeof(Home));
        }

        private void nvTopLevelNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }

        private void nvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(Settings));
            }
            else
            {
                TextBlock ItemContent = args.InvokedItem as TextBlock;
                if (ItemContent != null)
                {
                    switch (ItemContent.Tag)
                    {
                        case "Nav_Home":
                            contentFrame.Navigate(typeof(Home));
                            break;

                        case "Nav_AllLocations":
                            contentFrame.Navigate(typeof(AllLocations));
                            break;

                        case "Nav_About":
                            contentFrame.Navigate(typeof(About));
                            break;

                        case "Nav_Settings":
                            contentFrame.Navigate(typeof(Settings));
                            break;
                    }
                }
            }
        }
    }
}
