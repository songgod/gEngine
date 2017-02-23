using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util
{
    public class RelayCommand : System.Windows.Input.ICommand
    {
        #region 成员定义

        readonly Func<bool> _canExecute;
        readonly Action _execute;

        #endregion

        #region 构造函数

        /// <summary>
        /// 一个参数的构造函数
        /// </summary>
        /// <param name="execute"></param>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 两个参数的构造函数
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {

            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand 接口成员
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    System.Windows.Input.CommandManager.RequerySuggested+= value;
            }
            remove
            {
                if (_canExecute != null)
                    System.Windows.Input.CommandManager.RequerySuggested -= value;
            }
        }
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        } 
        #endregion
    }

    public class RelayCommand<T> : System.Windows.Input.ICommand
    {

        #region 成员定义

        readonly Predicate<T> _canExecute;
        readonly Action<T> _execute;

        #endregion

        #region 构造函数

        /// <summary>
        /// 一个参数的构造函数
        /// </summary>
        /// <param name="execute"></param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 两个参数的构造函数
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {

            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand 接口成员

        public event EventHandler CanExecuteChanged
        {
            add
            {

                if (_canExecute != null)
                    System.Windows.Input.CommandManager.RequerySuggested += value;
            }
            remove
            {

                if (_canExecute != null)
                    System.Windows.Input.CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }
}
