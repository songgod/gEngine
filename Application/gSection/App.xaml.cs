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
            gEngine.Graph.Interface.Registry.LoadMapReadWriter();
            gEngine.Graph.Interface.Registry.LoadLayerCreator();
            gEngine.Manipulator.Registry.LoadManipulators();
            gEngine.View.Registry.LoadLocalElement();
            gEngine.Project.Registry.LoadLocalElement();
            
            gEngine.Symbol.Registry.LoadLocalSymbols();
            //gEngine.Application.Registry.LoadLocalElement();
            gEngine.Graph.Tpl.Ge.Registry.LoadTemplate();
        }
    }
}