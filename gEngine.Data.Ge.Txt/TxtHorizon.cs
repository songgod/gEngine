using gEngine.Data.Interface;
using gEngine.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Ge.Txt
{
    public class TxtHorizon : DBHorizons
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
            double deaultvalue = -9999;
            string defaultstring = "defaultstring";

            var file = File.Open(txtfilepath, FileMode.Open);
            using (var stream = new StreamReader(file, Encoding.GetEncoding("gb2312")))
            {
                while (!stream.EndOfStream)
                {
                    string strLine = stream.ReadLine().Trim();
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        string[] strColumns = strLine.Split('\t');
                        if (colFlag)
                        {
                            curveCount = strColumns.Length;
                            colFlag = false;
                            for (int i = 4; i < curveCount; i++)
                            {
                                ColNames.Add(strColumns[i]);
                            }
                        }
                        else
                        {
                            IDBHorizon Horizon = new DBHorizon();
                            Horizon.Name = StringUtil.ValidString(strColumns[0], defaultstring);
                            Horizon.LayerNumber = StringUtil.ValidString(strColumns[1], defaultstring);
                            Horizon.Top_MeasuredDepth = NumUtil.ToDouble(strColumns[2], true, deaultvalue);
                            Horizon.MeasuredThickness = NumUtil.ToDouble(strColumns[3], true, deaultvalue);

                            for (int i = 4; i < curveCount; i++)
                            {
                                if (Type.IndexOf(i).Equals(-1))
                                {
                                    if (strColumns.Length > i)
                                    {
                                        if (!string.IsNullOrEmpty(strColumns[i]))
                                        {
                                            double result = NumUtil.ToDouble(strColumns[i]);
                                            Horizon.DHorizonDatas.Add(result);
                                        }
                                        else
                                        {
                                            Horizon.DHorizonDatas.Add(deaultvalue);
                                        }
                                    }
                                    else
                                    {
                                        Horizon.DHorizonDatas.Add(deaultvalue);
                                    }
                                }
                                else
                                {
                                    string value = defaultstring.ToString();
                                    if (strColumns.Length > i)
                                    {
                                        value = StringUtil.ValidString(strColumns[i], defaultstring);
                                    }
                                    Horizon.SHorizonDatas.Add(value);
                                }
                            }

                            Horizons.Add(Horizon);
                        }
                    }
                }
            }
            txtfile = txtfilepath;
            return true;
        }
    }
}
