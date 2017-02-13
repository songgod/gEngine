using Microsoft.Win32;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace gEngineTest
{
    /// <summary>
    /// UploadDataTemplate.xaml 的交互逻辑
    /// </summary>
    public partial class UploadDataTemplate : Window
    {
        Thread copyThread;
        OpenFileDialog openFile;
        public UploadDataTemplate()
        {
            InitializeComponent();
            copyProgress.Visibility = Visibility.Collapsed;
            var manager = new DataTemplateManager();
            cbTemplateType.ItemsSource = manager.GetDataTemplateTypes();
            cbTemplateType.SelectedIndex = 0;
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            ///设定要上传的文件全路径
            openFile = new OpenFileDialog();
            openFile.AddExtension = true;
            openFile.CheckPathExists = true;
            openFile.Filter = "*.xaml|*.xaml";
            openFile.FilterIndex = 0;
            openFile.Multiselect = false;
            openFile.Title = "选择模板";

            bool? f = openFile.ShowDialog();
            if (f != null && f.Value)
            {
                this.srcFile.Text = openFile.FileName;
            }
        }


        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            string sourcePath = this.srcFile.Text.Trim();
            string destDir = AppDomain.CurrentDomain.BaseDirectory + "DataTemplates\\" +
                                cbTemplateType.SelectedValue.ToString();

            if (!File.Exists(sourcePath))
            {
                MessageBox.Show("请选择模板！");
                return;
            }
            
            
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }
            string destPath = destDir + "\\" + System.IO.Path.GetFileName(sourcePath);

            if (File.Exists(destPath))
            {
                string messageBoxText = "模板已经存在,要覆盖吗?";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(messageBoxText, "提示", button);
                if (result == MessageBoxResult.No)
                    return;
            }

            this.btnUpload.IsEnabled = false;
            ///copy file and nodify ui that rate of progress of file copy          
            this.copyFlag.Text = "开始上传...";
            //设置进度条最大值
            this.copyProgress.Maximum = (new FileInfo(sourcePath)).Length;
            //保存复制文件信息，以进行传递
            CopyFileInfo c = new CopyFileInfo() { SourcePath = sourcePath, DestPath = destPath };
            //线程异步调用复制文件
            copyThread = new Thread(new ParameterizedThreadStart(CopyFile));
            copyThread.Start(c);
        }
        /// <summary>
        /// 上传文件的委托方法
        /// </summary>
        /// <param name="obj">复制文件的信息</param>
        private void CopyFile(object obj)
        {
            CopyFileInfo c = obj as CopyFileInfo;
            CopyFile(c.SourcePath, c.DestPath);
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        private void CopyFile(string sourcePath, string destPath)
        {
            FileInfo f = new FileInfo(sourcePath);
            FileStream fsR = f.OpenRead();
            FileStream fsW = File.Create(destPath);
            long fileLength = f.Length;
            byte[] buffer = new byte[1024];
            int n = 0;

            while (true)
            {
                ///设定线程优先级
                ///异步调用UpdateCopyProgress方法
                ///并传递2个long类型参数fileLength 与 fsR.Position
                this.displayCopyInfo.Dispatcher.Invoke(DispatcherPriority.SystemIdle,
                    new Action<long, long>(UpdateCopyProgress), fileLength, fsR.Position);

                //读写文件
                n = fsR.Read(buffer, 0, 1024);
                if (n == 0)
                {
                    break;
                }
                fsW.Write(buffer, 0, n);
                fsW.Flush();
                Thread.Sleep(1);
            }
            fsR.Close();
            fsW.Close();
            this.copyFlag.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                copyFlag.Text = "上传完成";
                copyProgress.Visibility = Visibility.Collapsed;
                this.btnUpload.IsEnabled = true;
            });
        }

        private void UpdateCopyProgress(long fileLength, long currentLength)
        {
            this.displayCopyInfo.Text = string.Format("总大小：{0:0,00}kb字节, 已上传:{1:0,00}字节", fileLength, currentLength);
            //刷新进度条     
            copyProgress.Visibility = Visibility.Visible;
            this.copyProgress.Value = currentLength;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
    public class CopyFileInfo
    {
        public string SourcePath { get; set; }
        public string DestPath { get; set; }
    }
}

