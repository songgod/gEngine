﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.Commands.OperateView
{
    public class CommandsOperateView
    {
        public static FullViewCommand FullViewCommand { get; set; }

        static CommandsOperateView()
        {
            FullViewCommand = new FullViewCommand();
        }
    }
}