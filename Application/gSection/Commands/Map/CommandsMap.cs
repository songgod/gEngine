using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.Commands.Map
{
    public class CommandsMap
    {
        static CommandsMap()
        {
            NewPlaneMapCommand = new NewPlaneMapCommand();
            NewSectionMapCommand = new NewSectionMapCommand();
        }
        public static NewPlaneMapCommand NewPlaneMapCommand { get; set; }
        public static NewSectionMapCommand NewSectionMapCommand { get; set; }
    }
}
