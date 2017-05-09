using gEngine.Graph.Ge;
using gEngine.Graph.Interface;
using gEngine.Util.Ge.Basic;
using gSection.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.Commands.OperateView
{
    public class ScaleRuleCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            ScaleRuleLayerCreator sc = new ScaleRuleLayerCreator();
            Layer layer = sc.Create();
            IMap map = Project.Single.NewMap("Ge", "ScaleRule");
            map.Layers.Add(layer);
        }
    }
}
