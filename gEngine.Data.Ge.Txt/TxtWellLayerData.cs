using gEngine.Data.Ge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Ge.Txt
{
    public class TxtWellLayerData : DBWellLayerData
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

        public bool ReadFromTxt(string txtfilepath)
        {
            if (txtfilepath == TxtFile)
                return true;
            int curveCount = 0;//定义曲线的条数
            bool colFlag = true;//曲线标题的标识

            var file = File.Open(txtfilepath, FileMode.Open);
            System.Text.RegularExpressions.Regex whitespace = new System.Text.RegularExpressions.Regex(@"\s+", System.Text.RegularExpressions.RegexOptions.Compiled);

            using (var stream = new StreamReader(file, Encoding.GetEncoding("gb2312")))
            {
                WellLayerDatas = new List<Tuple<string, List<string>>>();
                WellNames = new List<string>();
                while (!stream.EndOfStream)
                {
                    string strLine = whitespace.Replace(stream.ReadLine().Trim(), " ");
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        string[] strColumns = strLine.Split(' ');
                        if (colFlag)
                        {
                            curveCount = strColumns.Length;
                            colFlag = false;
                            for (int i = 0; i < curveCount; i++)
                            {
                                if (i.Equals(0))
                                {
                                    Name = strColumns[i];
                                }
                                else
                                {
                                    WellLayerDatas.Add(new Tuple<string, List<string>>(strColumns[i], new List<string>()));
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < curveCount; i++)
                            {
                                string value = string.Empty;
                                if (strColumns.Length > i)
                                {
                                     value = strColumns[i];
                                }
                                if (i.Equals(0))
                                {
                                    WellNames.Add(value);
                                }
                                else
                                {
                                    WellLayerDatas[i - 1].Item2.Add(value);
                                }
                            }
                        }
                    }
                }
            }
            txtfile = txtfilepath;
            return true;
        }
    }
}
