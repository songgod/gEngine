using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Interface;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator
{
    public class EditDefaultManipulator : ObjectManipulator
    {
        public Rectangle TrackAdorner { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;
            System.Windows.Media.Effects.DropShadowEffect effect =new System.Windows.Media.Effects.DropShadowEffect() ;
            effect.BlurRadius = 10;
            effect.Color = Colors.Blue;
            effect.ShadowDepth = 0;
            this.AssociatedObject.Effect = effect;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Effect = null;
        }
    }


    public class EditDefaultManipulatorFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditDefaultManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return null;
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new EditDefaultManipulator();
        }
    }
}
