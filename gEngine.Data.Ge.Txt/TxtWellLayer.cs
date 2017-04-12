using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Ge.Txt
{
    public class TxtWellLayer : DBWellLayers
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

            bool colFlag = true;//曲线标题的标识

            if (File.Exists(txtfilepath))
            {
                var file = File.Open(txtfilepath, FileMode.Open);
                System.Text.RegularExpressions.Regex whitespace = new System.Text.RegularExpressions.Regex(@"\s+", System.Text.RegularExpressions.RegexOptions.Compiled);
                using (var stream = new StreamReader(file, Encoding.GetEncoding("gb2312")))
                {
                    while (!stream.EndOfStream)
                    {
                        string strLine = whitespace.Replace(stream.ReadLine().Trim(), " ");
                        if (!string.IsNullOrEmpty(strLine))
                        {
                            string[] strLines = strLine.Split(' ');
                            if (colFlag)
                            {
                                colFlag = false;
                            }
                            else
                            {
                                if (strLines.Length < 4)
                                {
                                    continue;
                                }
                                if (!string.IsNullOrEmpty(strLines[2]) && !string.IsNullOrEmpty(strLines[3]))
                                {
                                    DBWellLayer dbWellLayer = new DBWellLayer();
                                    dbWellLayer.Name = strLines[0];
                                    dbWellLayer.BoundaryName = strLines[1];
                                    dbWellLayer.TopDepth = double.Parse(strLines[2]);
                                    dbWellLayer.Thickness = double.Parse(strLines[3]);
                                    this.Add(dbWellLayer);
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
