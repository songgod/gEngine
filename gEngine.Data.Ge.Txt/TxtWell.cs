using gEngine.Data.Ge.Txt.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Ge.Txt
{
    [TypeConverterAttribute(typeof(TxtWellConverter))]
    public class TxtWell : DBWell
    {
        private string txtfile;
        public string TxtFile
        {
            get
            {
                return txtfile;
            }
            set
            {
                ReadFromTxt(value);
            }
        }

        private bool IsDecimal(string numstr)
        {
            try
            {
                Decimal dt;
                dt = Convert.ToDecimal(numstr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReadFromTxt(string txtfilepath)
        {
            if (txtfilepath == TxtFile)
                return true;
            int curveCount = 0;//定义曲线的条数
            bool colFlag = true;//曲线标题的标识

            var file = File.Open(txtfilepath, FileMode.Open);

            using (var stream = new StreamReader(file, Encoding.GetEncoding("gb2312")))
            {
                Columns = new List<Tuple<string, List<double>>>();
                Depths = new List<double>();
                while (!stream.EndOfStream)
                {
                    string strLine = System.Text.RegularExpressions.Regex.Replace(stream.ReadLine().Trim().ToString(), @"\s+", " ");
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        string[] strColumns = strLine.Split(' ');
                        curveCount = strColumns.Length;
                        if (colFlag)
                        {
                            colFlag = false;
                            for (int i = 0; i < curveCount; i++)
                            {
                                if (i.Equals(0))
                                {
                                    Name = strColumns[i];
                                }
                                else
                                {
                                    Columns.Add(new Tuple<string, List<double>>(strColumns[i], new List<double>()));
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < curveCount; i++)
                            {
                                if (!string.IsNullOrEmpty(strColumns[i]) && IsDecimal(strColumns[i]))
                                {
                                    if (i.Equals(0))
                                    {
                                        Depths.Add(double.Parse(strColumns[i]));
                                    }
                                    else
                                    {
                                        double xValue = double.Parse(strColumns[i]);
                                        Columns[i - 1].Item2.Add(xValue);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            txtfile = txtfilepath;
            return true;
        }

        public bool SaveToTxt(string file)
        {
            throw new NotImplementedException();
        }
    }
}
