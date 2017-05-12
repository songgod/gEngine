using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace gEngineTest.Boundary
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            gEngine.Data.Interface.Register.LoadDBFactorys();
            gEngine.Graph.Interface.Registry.LoadReadWriter();
            gEngine.Manipulator.Registry.LoadManipulators();
            //Project.NewProject();
            //Project.Single.OpenDBSource(@"D:\gSectionData.Txt");
            gEngine.View.Registry.LoadLocalElement();
        }
    }
}
