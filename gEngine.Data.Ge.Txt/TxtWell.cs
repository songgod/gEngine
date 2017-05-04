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

        private bool IsDecimal(string numstr, out double result)
        {
            try
            {
                double dt;
                dt = Convert.ToDouble(numstr);
                result = dt;
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        public bool ReadFromTxt(string txtfilepath)
        {
            if (txtfilepath == TxtFile)
                return true;
            int curveCount = 0;//定义曲线的条数
            bool colFlag = true;//曲线标题的标识

            if (!File.Exists(txtfilepath))
                return false;

            Name = System.IO.Path.GetFileNameWithoutExtension(txtfilepath);
            Columns = new List<Tuple<string, List<double>>>();
            Depths = new List<double>();

            var file = File.Open(txtfilepath, FileMode.Open);

            System.Text.RegularExpressions.Regex whitespace = new System.Text.RegularExpressions.Regex(@"\s+", System.Text.RegularExpressions.RegexOptions.Compiled);

            using (var stream = new StreamReader(file, Encoding.GetEncoding("gb2312")))
            {
                while (!stream.EndOfStream)
                {
                    //string strLine = System.Text.RegularExpressions.Regex.Replace(stream.ReadLine().Trim().ToString(), @"\s+", " ");
                    string strLine = whitespace.Replace(stream.ReadLine().Trim(), " ");
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        string[] strColumns = strLine.Split(' ');
                        curveCount = strColumns.Length;
                        if (colFlag)
                        {
                            colFlag = false;
                            for (int i = 1; i < curveCount; i++)
                            {
                                Columns.Add(new Tuple<string, List<double>>(strColumns[i], new List<double>()));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < curveCount; i++)
                            {
                                double value;
                                if (IsDecimal(strColumns[i], out value))
                                {
                                    if (i.Equals(0))
                                    {
                                        if (value > 0)
                                            Depths.Add(value);
                                    }
                                    else
                                    {
                                        Columns[i - 1].Item2.Add(value);
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
