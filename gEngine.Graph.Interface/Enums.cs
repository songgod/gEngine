using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Interface
{
    public class Enums
    {
        /// <summary>
        /// 枚举：井别
        /// </summary>
        public enum WellType
        {
            /// <summary>
            /// 油井
            /// </summary>
            Y,
            /// <summary>
            /// 水井
            /// </summary>
            W
        }
        /// <summary>
        /// 枚举：井类
        /// </summary>
        public enum WellCategory
        {
            /// <summary>
            /// 类别1
            /// </summary>
            WellCg0=0,
            /// <summary>
            /// 类别2
            /// </summary>
            WellCg1 = 1
        }

    }
}
