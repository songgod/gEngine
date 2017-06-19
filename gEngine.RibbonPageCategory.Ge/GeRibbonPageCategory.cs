using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Ribbon;

namespace gEngine.RibbonPageCategory.Ge
{
    public  class GeRibbonPageCategory: DevExpress.Xpf.Ribbon.RibbonPageCategory
    {
        public virtual Type SupportType { get; }
    }
}
