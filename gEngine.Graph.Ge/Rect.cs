using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge
{
    public class Rect : Object, IRect
    {
        private double top;
        public double Top
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
                RaiseProertyChanged("Top");
            }
        }

        private double left;
        public double Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
                RaiseProertyChanged("Left");
            }
        }

        private double width;
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                RaiseProertyChanged("Width");
            }
        }

        private double height;
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                RaiseProertyChanged("Height");
            }
        }

    }
}
