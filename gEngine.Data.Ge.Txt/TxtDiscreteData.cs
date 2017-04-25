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
    public class TxtDiscreteData : DBDiscreteDatas
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
            double outResult = deaultvalue;

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
                            for (int i = 5; i < curveCount; i++)
                            {
                                ColNames.Add(strColumns[i]);
                            }
                        }
                        else
                        {
                            IDBDiscreteData DiscreteData = new DBDiscreteData();
                            DiscreteData.Name = StringUtil.ValidString(strColumns[0],defaultstring);
                            DiscreteData.LayerNumber = StringUtil.ValidString(strColumns[1], defaultstring);
                            DiscreteData.SerialNumber = StringUtil.ValidString(strColumns[2], defaultstring);
                            DiscreteData.Top_MeasuredDepth = NumUtil.ToDouble(strColumns[3], true, deaultvalue);
                            DiscreteData.MeasuredThickness = NumUtil.ToDouble(strColumns[4], true, deaultvalue);
                            for (int i = 5; i < curveCount; i++)
                            {
                                if (Type.IndexOf(i).Equals(-1))
                                {
                                    if (strColumns.Length > i)
                                    {
                                        //double value = NumUtil.ToDouble(strColumns[i], true, deaultvalue);
                                        //DiscreteData.DDiscreteDatas.Add(value);
                                        if (!string.IsNullOrEmpty(strColumns[i]))
                                        {
                                            double result = NumUtil.ToDouble(strColumns[i], out outResult) == true ? outResult : deaultvalue;
                                            DiscreteData.DDiscreteDatas.Add(result);
                                        }
                                        else
                                        {
                                            DiscreteData.DDiscreteDatas.Add(deaultvalue);
                                        }
                                    }
                                    else
                                    {
                                        DiscreteData.DDiscreteDatas.Add(deaultvalue);
                                    }
                                }
                                else
                                {
                                    string value = defaultstring.ToString();
                                    if (strColumns.Length > i)
                                    {
                                        value = StringUtil.ValidString(strColumns[i], defaultstring);
                                    }
                                    DiscreteData.SDiscreteDatas.Add(value);
                                }
                            }

                            DiscreteDatas.Add(DiscreteData);
                        }
                    }
                }
            }
            txtfile = txtfilepath;
            return true;
        }
    }
}
