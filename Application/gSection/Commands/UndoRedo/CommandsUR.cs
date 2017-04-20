using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.Commands.UndoRedo
{
    public class CommandsUR
    {
        public static UndoCommand UndoCommand { get; set; }
        public static RedoCommand RedoCommand { get; set; }
        static CommandsUR()
        {
            UndoCommand = new UndoCommand();
            RedoCommand = new RedoCommand();
        }
    }
}
