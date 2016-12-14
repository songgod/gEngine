using gEngine.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace gEngine.Data.Ge.Txt.Converter
{
    public class TextDBWellLocationsConverter : TypeConverter
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
                TXTWellLocations welllocations = new TXTWellLocations();
                welllocations.ReadFromTxt(filePath);
               
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

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(TXTWellLocations))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is TXTWellLocations)
            {
                TXTWellLocations wls = value as TXTWellLocations;
                wls.SaveToTxt(wls.TxtFile);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
