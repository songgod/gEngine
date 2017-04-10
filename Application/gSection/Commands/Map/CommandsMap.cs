using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.Commands
{
    public class CommandsInstance
    {
        static CommandsInstance()
        {
            NewPlaneMapCommand = new NewPlaneMapCommand();
        }
        public static NewPlaneMapCommand NewPlaneMapCommand { get; set; }
    }
}
