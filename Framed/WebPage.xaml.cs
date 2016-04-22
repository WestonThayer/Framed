﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Framed
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebPage : Page
    {
        private Settings settings;
        private bool isLoaded; // If true, we loaded our first page already

        public WebPage()
        {
            this.InitializeComponent();
            this.settings = new Settings();

            SystemNavigationManager.GetForCurrentView().BackRequested += Nav_BackRequested;
            ApplicationView.GetForCurrentView().FullScreenSystemOverlayMode = FullScreenSystemOverlayMode.Minimal;
        }

        private void Nav_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // This event handler will occasionally be called twice, so it's important
            // to check if we've handled it already
            if (!e.Handled)
            {
                if (MyWebView.CanGoBack)
                {
                    e.Handled = true;
                    MyWebView.GoBack();
                }
                else if (this.Frame.CanGoBack)
                {
                    e.Handled = true;
                    this.Frame.GoBack();
                }
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.isLoaded = false;

            string url = e.Parameter as string;
            Uri urlUri = null;

            try
            {
                urlUri = new Uri(url);
            }
            catch (UriFormatException)
            {
                if (url.IndexOf("http://") != 0)
                {
                    url = "http://" + url;
                }
            }

            if (urlUri == null)
            {
                urlUri = new Uri(url);
            }

            if (this.settings.IsFullScreen)
            {
                ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
            }

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            // Make sure the user always sees a blank page
            MyWebView.Navigate(new Uri("ms-appx-web:///landingpage.html"));
            await Task.Delay(300); // Make sure it ends up on WebView's back stack
            MyWebView.Navigate(new Uri(url)); // Finally, go to what they asked for

            this.isLoaded = true;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            if (this.settings.IsFullScreen)
            {
                ApplicationView.GetForCurrentView().ExitFullScreenMode();
            }

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private void MyWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            LoadingProgressBar.Visibility = Visibility.Visible;
        }

        private async void MyWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            LoadingProgressBar.Visibility = Visibility.Collapsed;

            if (!args.IsSuccess)
            {
                ContentDialog d = new ContentDialog();
                d.Title = "Oops";
                d.Content = "Navigation to " + args.Uri.ToString() + " failed. " + args.WebErrorStatus.ToString();
                d.PrimaryButtonText = "Ok";

                await d.ShowAsync();
            }

            // Check for the landing page so that the user doesn't have to press
            // back an extra time
            string m = args.Uri.Scheme + "://" + args.Uri.AbsolutePath;
            if (this.isLoaded && m == "ms-appx-web:///landingpage.html")
            {
                if (this.Frame.CanGoBack)
                {
                    this.Frame.GoBack();
                }
            }
        }
    }
}
