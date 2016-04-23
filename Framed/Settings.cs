using Framed.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;

namespace Framed
{
    public class Settings
    {
        public bool IsFullScreen
        {
            get
            {
                var v = ApplicationData.Current.LocalSettings.Values["IsFullScreen"] as bool?;
                return v ?? false;
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["IsFullScreen"] = value;
            }
        }

        public bool IsTitleBarTransparent
        {
            get
            {
                var v = ApplicationData.Current.LocalSettings.Values["IsTitleBarTransparent"] as bool?;
                return v ?? false;
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["IsTitleBarTransparent"] = value;
            }
        }

        public Color TitleBarButtonBackground
        {
            get
            {
                string v = ApplicationData.Current.LocalSettings.Values["TitleBarButtonBackgroundColor"] as string;

                if (v != null)
                {
                    return ColorExtensions.Parse(v);
                }
                else
                {
                    return Color.FromArgb(0, 0, 0, 0);
                }
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["TitleBarButtonBackgroundColor"] = value.ToString();
            }
        }

        public Color TitleBarButtonForeground
        {
            get
            {
                string v = ApplicationData.Current.LocalSettings.Values["TitleBarButtonForegroundColor"] as string;

                if (v != null)
                {
                    return ColorExtensions.Parse(v);
                }
                else
                {
                    return Color.FromArgb(0, 0, 0, 0);
                }
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["TitleBarButtonForegroundColor"] = value.ToString();
            }
        }

        public bool IsKeyboardShortcutsEnabled
        {
            get
            {
                var v = ApplicationData.Current.LocalSettings.Values["IsKeyboardShortcutsEnabled"] as bool?;
                return v ?? true; // Turn them on by default
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["IsKeyboardShortcutsEnabled"] = value;
            }
        }

        public bool IsCameraShortcutEnabled
        {
            get
            {
                var v = ApplicationData.Current.LocalSettings.Values["IsCameraShortcutEnabled"] as bool?;
                return v ?? true; // Turn it on by default
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["IsCameraShortcutEnabled"] = value;
            }
        }

        public Settings()
        {

        }
    }
}
