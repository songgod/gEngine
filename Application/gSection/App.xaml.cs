using gSection.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using gEngine.Data.Interface;

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
            Register.LoadCreater();
            Project.NewProject();
            string dbpath = @"D:\gSectionData.Txt";
            Project.Single.DBTuple = new Project.DBFactoryTuple(dbpath, Register.CreateDBFactory(dbpath));
        }
    }
}