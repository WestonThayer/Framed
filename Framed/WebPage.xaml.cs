using System;
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
        public WebPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += Nav_BackRequested;
        }

        private async void Nav_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // Delay ever so slightly to give WebView a chance to correctly report
            // CanGoBack
            await Task.Delay(100);

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

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

            MyWebView.Navigate(new Uri(url));

            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            ApplicationView.GetForCurrentView().ExitFullScreenMode();
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
        }
    }
}
