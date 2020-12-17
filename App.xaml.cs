using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace EasySaveG5Graphic
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var langCode = EasySaveG5Graphic.Properties.Settings.Default.LangueCode;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langCode);
            base.OnStartup(e);
        }
    }
}
