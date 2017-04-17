using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util
{
    public interface IUndoRedoCommand
    {
        /// <summary>
        /// 撤销
        /// </summary>
        void Undo();

        /// <summary>
        /// 重做
        /// </summary>    
        void Redo();
    }
}
