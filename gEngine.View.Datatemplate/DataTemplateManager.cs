using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using static gEngine.Graph.Interface.Enums;

namespace gEngine.View.Datatemplate
{
    /// <summary>
    /// 模板管理类
    /// </summary>
    public class DataTemplateManager
    {
        /// <summary>
        /// 按类型注册模板
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TView"></typeparam>
        public void RegisterDataTemplate<TViewModel, TView>() where TView : FrameworkElement
        {
            RegisterDataTemplate(typeof(TViewModel), typeof(TView));
        }

        public void RegisterDataTemplate(Type viewModelType, Type viewType)
        {
            var template = CreateTemplate(viewModelType, viewType);
            var key = template.DataTemplateKey;
            if (!Application.Current.Resources.Contains(key))
                Application.Current.Resources.Add(key, template);
            else
                Application.Current.Resources[key] = template;
        }

        /// <summary>
        /// 功能：创建数据模板方式1
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        private DataTemplate CreateTemplate(Type viewModelType, Type viewType)
        {
            const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><v:{1} /></DataTemplate>";
            var xaml = String.Format(xamlTemplate, viewModelType.Name, viewType.Name);
            var context = new ParserContext();

            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace, viewModelType.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction("v", viewType.Namespace, viewType.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("v", "v");

            var template = (DataTemplate)XamlReader.Parse(xaml, context);
            return template;

        }

        /// <summary>
        /// 按xaml文件名注册数据模板
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="fileName">文件全路径</param>
        public void RegDataTemplateByFile<TViewModel>(string type, string fileName)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "DataTemplates\\" + type + "\\" + fileName;
            StreamReader reader = new StreamReader(filePath, Encoding.Default);

            string fileContent = reader.ReadToEnd();
            var viewModelType = typeof(TViewModel);
            var template = CreateTemplate(viewModelType, fileContent);
            var key = template.DataTemplateKey;
            if (!Application.Current.Resources.Contains(key))
                Application.Current.Resources.Add(key, template);
            else
                Application.Current.Resources[key] = template;
        }

        /// <summary>
        /// 功能：创建数据模板方式2
        /// </summary>
        /// <param name="viewModelType">viewModel类型</param>
        /// <param name="fileContent">模板文件内容</param>
        /// <returns></returns>
        private DataTemplate CreateTemplate(Type viewModelType, string fileContent)
        {
            const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\">{1}</DataTemplate>";
            var xaml = String.Format(xamlTemplate, viewModelType.Name, fileContent);
            var context = new ParserContext();
            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace, viewModelType.Assembly.FullName);
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            var template = (DataTemplate)XamlReader.Load(new MemoryStream(Encoding.UTF8.GetBytes(xaml)), context);

            return template;
        }

        public List<string> GetAllDataTemplates()
        {
            List<string> result = new List<string>();
            string templateFolder = AppDomain.CurrentDomain.BaseDirectory + "DataTemplates\\";
            DirectoryInfo TheFolder = new DirectoryInfo(templateFolder);
            foreach (FileInfo file in TheFolder.GetFiles())
            {
                if (file.Extension.Equals(".xaml"))
                {
                    result.Add(file.Name);
                }
            }

            return result;
        }

        public List<string> GetAllDataTemplatesByType(string type)
        {
            List<string> result = new List<string>();
            string templateFolder = AppDomain.CurrentDomain.BaseDirectory + "DataTemplates\\" + type + "\\";
            DirectoryInfo TheFolder = new DirectoryInfo(templateFolder);
            foreach (FileInfo file in TheFolder.GetFiles())
            {
                if (file.Extension.Equals(".xaml"))
                {
                    result.Add(file.Name);
                }
            }
            return result;
        }

        public List<string> GetDataTemplateTypes()
        {
            List<string> result = new List<string>();
            result = Enum.GetNames(typeof(DataTemplateType)).ToList<string>();
            return result;
        }

    }
}
