using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

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

        public Settings()
        {

        }
    }
}
