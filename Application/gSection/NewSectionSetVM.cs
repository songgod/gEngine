using gEngine.Util;
using GPTDxWPFRibbonApplication1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GPTDxWPFRibbonApplication1
{
    public class NewSectionSetVM
    {
        public NewSectionSetVM()
        {

        }

        #region Method

        /// <summary>
        /// 确定执行函数
        /// </summary>
        /// <param name="w"></param>
        private void Confirm(Window w)
        {
            Button ok = w.FindName("button") as Button;
            //MessageBox.Show(ok.Name);

            w.Close();

            RibbonViewModel s = new RibbonViewModel();

            s.OpenTab("GPTDxWPFRibbonApplication1.Controls.DWellControl");
        }

        /// <summary>
        /// 取消执行按钮
        /// </summary>
        /// <param name="w"></param>
        private void Cancel(Window w)
        {
            if (w != null)
            {
                w.Close();
            }
        }

        #endregion

        #region ICommand

        /// <summary>
        /// 确定命令
        /// </summary>
        public System.Windows.Input.ICommand ConfirmCommand
        {
            get { return new RelayCommand<Window>(Confirm); }
        }

        public System.Windows.Input.ICommand CancelCommand
        {
            get { return new RelayCommand<Window>(Cancel); }
        }

        #endregion
    }
}
