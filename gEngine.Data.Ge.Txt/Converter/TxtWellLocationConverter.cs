using gEngine.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Ge.Txt.Converter
{
    public class TextDBWellLocationConverter : TypeConverter
    {
        /// <summary>
        /// 功能：将txt文件路径转化为实体类存储
        /// 将数据存储在WellLocationDataSource类中的Objects中
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string filePath = value.ToString();//文件路径
                WellLocationGes welllocations = new WellLocationGes();

               
                return welllocations;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
    }
}
