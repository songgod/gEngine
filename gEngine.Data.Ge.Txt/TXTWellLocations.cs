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
    [TypeConverterAttribute(typeof(TextDBWellLocationsConverter))]
    public class TXTWellLocations : DBWellLocations
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
        public bool ReadFromTxt(string file)
        {
            if (file == txtfile)
                return true;

            System.IO.StreamReader reader = new StreamReader(file, Encoding.Default);
            int lineCount = 0;
            while (reader.Peek() > 0)
            {
                string curLine = reader.ReadLine();
                if (lineCount > 0)
                {//从第2行开始读
                    DBWellLocation well = new DBWellLocation();
                    string[] arrLine = curLine.Split('\t');
                    well.Name = arrLine[0];
                    well.WellCategory = String.IsNullOrEmpty(arrLine[1]) ? 0 : int.Parse(arrLine[1]);
                    well.WellType = arrLine[2];
                    well.x = double.Parse(arrLine[3]);
                    well.y = double.Parse(arrLine[4]);
                    Add(well);
                }
                lineCount++;
            }
            txtfile = file;
            return true;
        }

        public bool SaveToTxt(string file)
        {
            throw new NotImplementedException();
        }
    }
}
