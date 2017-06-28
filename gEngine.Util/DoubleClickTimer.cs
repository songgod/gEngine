using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace gEngine.Util
{
    public static class DoubleClickTimer
    {
        //双击事件定时器  
        private static DispatcherTimer _timer;
        //是否单击过一次  
        private static bool _isFirst;

        static DoubleClickTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            _timer.Tick += new EventHandler(_timer_Tick);
        }

        /// <summary>  
        /// 判断是否双击  
        /// </summary>  
        /// <returns></returns>  
        public static bool IsDoubleClick()
        {
            if (!_isFirst)
            {
                _isFirst = true;
                _timer.Start();
                return false;
            }
            else
            {
                return true;
            }
        }

        //间隔时间  
        static void _timer_Tick(object sender, EventArgs e)
        {
            _isFirst = false;
            _timer.Stop();
        }
    }
}
