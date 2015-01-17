using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace SimpleTest
{
    class CsvController
    {
        static readonly CsvConfiguration CsvConfig = new CsvConfiguration{Delimiter=".", HasHeaderRecord = false}; 

        public static void ReadCsv(string fullPath)
        {
            using (var csvParser = new CsvParser(new StreamReader(fullPath), CsvConfig))
            {
                while (true)
                {
                    var row = csvParser.Read();
                    if (row == null)
                    {
                        break;
                    }
                    row.Reverse();                    
                }
            }
        }
    }
}
