using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;

namespace SimpleTest
{
    class Program
    {
        static readonly CsvConfiguration CsvConfig = new CsvConfiguration { Delimiter = ".", HasHeaderRecord = false};
        
        private static  string _fullPath;

        public static string FullPath
        {
            get { return _fullPath ?? (_fullPath = "domains.csv"); }
        }

        public static BlackListNode ReadBlackListFromCsv(string path)
        {
            using (var csvParser = new CsvParser(new StreamReader(path), CsvConfig))
            {
                var blackList = new BlackListNode(".");

                while (true)
                {
                    var row = csvParser.Read();

                    if (row == null)
                    {
                        break;
                    } 
                  
                   blackList.AddFqdnArray(row.Reverse().ToArray());
                }

                return blackList;
            }
        }

        public static IEnumerable<string> GetRandomTestValues(string path)
        {
            var randomUrl = new List<string>();
            var lines = File.ReadAllLines(path);         
            var rand = new Random();

            for (var i = 1; i <= 1000; i++)
            {
                randomUrl.Add(lines[rand.Next(lines.Length)]);
            }

            return randomUrl;
        }

        static void Main(string[] args)
        {
            if (args.Length != 0)
            {                
                _fullPath = args[0];
            }

            if (!File.Exists(FullPath))
            {
                Console.WriteLine("Error. File {0} not found!", FullPath);
                Console.Read();
                return;
            }

            var blackList = ReadBlackListFromCsv(FullPath);          
            var testValues = GetRandomTestValues(FullPath).ToList();         

            foreach (var testValue in testValues)
            {
                Console.WriteLine("Is {0} blacklisted? {1}", testValue,blackList.ContainsUrl(testValue));
            }

            Console.ReadKey();

            var blackList1 = new BlackListNode(".");
            blackList1.AddFullDomainName("b.c.com");
            blackList1.AddFullDomainName("a.b.c.com");         

            Console.WriteLine("Test 1: {0}", blackList1.ContainsUrl("a.b.c.com"));
            Console.WriteLine("Test 2: {0}", blackList1.ContainsUrl("d.c.com"));
            Console.WriteLine("Test 3: {0}", blackList1.ContainsUrl("b.c.com"));
            Console.WriteLine("Test 4: {0}", blackList1.ContainsUrl("c.com"));
            Console.WriteLine("Test 5: {0}", blackList1.ContainsUrl("nob.net"));

            Console.ReadKey();
        }
    }
}
