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
            gEngine.Manipulator.Registry.LoadManipulators();
            gEngine.View.Registry.LoadLocalElement();
            gEngine.Project.Registry.LoadLocalElement();
            gEngine.RibbonPageCategory.Registry.LoadLocalElement();
        }
    }
}