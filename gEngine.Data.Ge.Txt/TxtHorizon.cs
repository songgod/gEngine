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
                            double outResult;
                            IDBHorizon Horizon = new DBHorizon();
                            Horizon.Name = string.IsNullOrEmpty(strColumns[0]) == true ? WebUtil.Single.NullValue.ToString() : strColumns[0];
                            Horizon.LayerNumber = string.IsNullOrEmpty(strColumns[1]) == true ? WebUtil.Single.NullValue.ToString() : strColumns[1];
                            Horizon.Top_MeasuredDepth = WebUtil.Single.IsDecimal(strColumns[2], out outResult) == true ? outResult : WebUtil.Single.NullValue;
                            Horizon.MeasuredThickness = WebUtil.Single.IsDecimal(strColumns[3], out outResult) == true ? outResult : WebUtil.Single.NullValue;

                            for (int i = 4; i < curveCount; i++)
                            {
                                if (Type.IndexOf(i).Equals(-1))
                                {
                                    if (strColumns.Length > i)
                                    {
                                        double value = string.IsNullOrEmpty(strColumns[i]) == true ? WebUtil.Single.NullValue : double.Parse(strColumns[i]);
                                        Horizon.DHorizonDatas.Add(value);
                                    }
                                    else
                                    {
                                        Horizon.DHorizonDatas.Add(WebUtil.Single.NullValue);
                                    }
                                }
                                else
                                {
                                    string value = WebUtil.Single.NullValue.ToString();
                                    if (strColumns.Length > i)
                                    {
                                        value = string.IsNullOrEmpty(strColumns[i]) == true ? value : strColumns[i];
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
