using gSection.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace gSection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            gEngine.Data.Interface.Register.LoadDBFactorys();
            gEngine.Graph.Interface.Registry.LoadReadWriter();
            Project.NewProject();
            Project.Single.OpenDBSource(@"D:\gSectionData.Txt");
            gEngine.View.Registry.LoadLocalElement();
        }
    }
}