using Framed.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public bool IsPreferredWindowSizeEnabled
        {
            get
            {
                var v = ApplicationData.Current.LocalSettings.Values["IsPreferredWindowSizeEnabled"] as bool?;
                return v ?? false;
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["IsPreferredWindowSizeEnabled"] = value;
            }
        }

        public int PreferredWindowWidth
        {
            get
            {
                var v = ApplicationData.Current.LocalSettings.Values["PreferredWindowWidth"] as int?;
                return v ?? 500;
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["PreferredWindowWidth"] = value;
            }
        }

        public int PreferredWindowHeight
        {
            get
            {
                var v = ApplicationData.Current.LocalSettings.Values["PreferredWindowHeight"] as int?;
                return v ?? 400;
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["PreferredWindowHeight"] = value;
            }
        }

        public List<SavedLink> History
        {
            get
            {
                List<SavedLink> history = new List<SavedLink>();

                for (int i = 0; i < 3; i++)
                {
                    var documentTitle = ApplicationData.Current.LocalSettings.Values["HistorySavedLinkDocumentTitle" + i] as string;
                    var url = ApplicationData.Current.LocalSettings.Values["HistorySavedLinkUrl" + i] as string;

                    if (url != null)
                    {
                        history.Add(new SavedLink()
                        {
                            DocumentTitle = documentTitle,
                            Url = url
                        });
                    }
                }

                return history;
            }
        }

        public Settings()
        {

        }

        public void AddToHistory(SavedLink value)
        {
            if (value != null &&
                value.Url != null &&
                value.Url != string.Empty)
            {
                var documentTitle0 = ApplicationData.Current.LocalSettings.Values["HistorySavedLinkDocumentTitle0"] as string;
                var url0 = ApplicationData.Current.LocalSettings.Values["HistorySavedLinkUrl0"] as string;

                // Only replace if this is a new URL
                if (value.Url != url0)
                {
                    // Move slot 1 to 2, 0 to 1
                    for (int i = 1; i >= 0; i--)
                    {
                        var documentTitle = ApplicationData.Current.LocalSettings.Values["HistorySavedLinkDocumentTitle" + i] as string;
                        var url = ApplicationData.Current.LocalSettings.Values["HistorySavedLinkUrl" + i] as string;

                        ApplicationData.Current.LocalSettings.Values["HistorySavedLinkDocumentTitle" + (i + 1)] = documentTitle;
                        ApplicationData.Current.LocalSettings.Values["HistorySavedLinkUrl" + (i + 1)] = url;
                    }

                    // Replace slot 0
                    ApplicationData.Current.LocalSettings.Values["HistorySavedLinkDocumentTitle0"] = !string.IsNullOrWhiteSpace(value.DocumentTitle) ? value.DocumentTitle : value.Url;
                    ApplicationData.Current.LocalSettings.Values["HistorySavedLinkUrl0"] = value.Url;
                }
            }
            else
            {
                throw new ArgumentException("No null or empty URLs allowed!");
            }
        }

        public void ClearHistory()
        {
            for (int i = 0; i < 3; i++)
            {
                ApplicationData.Current.LocalSettings.Values["HistorySavedLinkDocumentTitle" + i] = null;
                ApplicationData.Current.LocalSettings.Values["HistorySavedLinkUrl" + i] = null;
            }
        }
    }

    public class SavedLink
    {
        public string DocumentTitle { get; set; }
        public string Url { get; set; }
    }
}
