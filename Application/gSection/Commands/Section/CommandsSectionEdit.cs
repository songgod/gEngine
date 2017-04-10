using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.Commands.Section
{
    public class CommandsSectionEdit
    {
        static CommandsSectionEdit()
        {
            EditLineCommand = new EditLineCommand();
            EraseFaceCommand = new EraseFaceCommand();
            EraseLineCommand = new EraseLineCommand();
            NewCurveCommand = new NewCurveCommand();
            NewFaultCommand = new NewFaultCommand();
            NewLineCommand = new NewLineCommand();
            ReplaceLineCommand = new ReplaceLineCommand();
        }

        public static EditLineCommand EditLineCommand { get; set; }
        public static EraseFaceCommand EraseFaceCommand { get; set; }
        public static EraseLineCommand EraseLineCommand { get; set; }
        public static NewCurveCommand NewCurveCommand { get; set; }
        public static NewFaultCommand NewFaultCommand { get; set; }
        public static NewLineCommand NewLineCommand { get; set; }
        public static ReplaceLineCommand ReplaceLineCommand { get; set; }
    }
}
