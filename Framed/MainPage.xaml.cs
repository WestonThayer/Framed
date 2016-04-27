﻿using Framed.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
        private Regex argbRx = new Regex("#[0-9a-fA-F]{8}");
        private Regex rgbRx = new Regex("#[0-9a-fA-F]{6}");

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

            ARGBColorTextBox.Text = this.Settings.TitleBarButtonBackground.ToString();
            RGBColorTextBox.Text = this.Settings.TitleBarButtonForeground.ToString(true);
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



        private void ARGBColorTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Length > 9 || !argbRx.IsMatch(tb.Text))
            {
                TitleBarButtonBackgroundErrorTextBlock.Text = "Invalid color";
            }
            else
            {
                TitleBarButtonBackgroundErrorTextBlock.Text = string.Empty;
            }
        }

        private void ARGBColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Length == 9 && argbRx.IsMatch(tb.Text))
            {
                this.Settings.TitleBarButtonBackground = ColorExtensions.Parse(tb.Text);
            }
        }

        private void RGBColorTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Length > 7 || !rgbRx.IsMatch(tb.Text))
            {
                TitleBarButtonForegroundErrorTextBlock.Text = "Invalid color";
            }
            else
            {
                TitleBarButtonForegroundErrorTextBlock.Text = string.Empty;
            }
        }

        private void RGBColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Length == 7 && rgbRx.IsMatch(tb.Text))
            {
                this.Settings.TitleBarButtonForeground = ColorExtensions.Parse(tb.Text);
            }
        }
    }
}
