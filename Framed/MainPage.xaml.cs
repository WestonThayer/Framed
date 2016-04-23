using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Framed
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool isAdvancedVisible;

        public Settings Settings;

        public MainPage()
        {
            this.InitializeComponent();
            this.isAdvancedVisible = false;
            this.Settings = new Settings();

            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                IsCameraShortcutEnabledCheckBox.Visibility = Visibility.Visible;
            }
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            nav();
        }

        private void UrlTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter) { nav(); }
        }

        private void nav()
        {
            this.Frame.Navigate(typeof(WebPage), UrlTextBox.Text);
        }

        private void ShowAdvancedButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            if (!this.isAdvancedVisible)
            {
                this.isAdvancedVisible = true;
                b.Content = "Hide advanced";
                AdvancedStackPanel.Visibility = Visibility.Visible;
                AdvancedStackPanelShowStoryboard.Begin();
            }
            else
            {
                this.isAdvancedVisible = false;
                b.Content = "Show advanced";
                AdvancedStackPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
