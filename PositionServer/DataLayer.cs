using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Threading;

namespace PositionServer
{
    public static class DataLayer
    {
        private static object sync = new object();

        public async static Task<IEnumerable<Security>> GetSecuritiesAsync()
        {
            string data = File.ReadAllText(@"C:\code\Elliott\PositionMonitor\PositionServer\securities.csv");
            string[] lines = data.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);

            //first line header
            var securities = new List<Security>();
            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    string[] attrs = ParseCsv(lines[i]);
                    if (attrs.Length == 4)
                    {
                        var sec = new Security();
                        int id;
                        if (int.TryParse(attrs[0], out id))
                        {
                            sec.ID = id;
                        }
                        else
                        {
                            continue;
                        }

                        sec.Symbol = attrs[1];
                        sec.Name = attrs[2];
                        sec.Sector = attrs[3];

                        securities.Add(sec);
                    }
                }
            }
            return await Task.Run(() => securities);
        }

        public async static Task<IEnumerable<Trade>> GetTradesAsync()
        {
            string data = File.ReadAllText(@"C:\code\Elliott\PositionMonitor\PositionServer\trades.csv"); //TODO - async read
            string[] lines = data.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);

            //first line header
            var trades = new List<Trade>();
            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    string[] attrs = ParseCsv(lines[i]);

                    if (attrs.Length == 4)
                    {
                        var trade = new Trade();
                        DateTime tradeDate;
                        if (DateTime.TryParse(attrs[0], out tradeDate))
                        {
                            trade.TradeDate = tradeDate;
                        }
                        else
                        {
                            continue;
                        }
                        int id;
                        if (int.TryParse(attrs[1], out id))
                        {
                            trade.SecurityId = id;
                        }
                        else
                        {
                            continue;
                        }
                        double price;
                        if (double.TryParse(attrs[2], out price))
                        {
                            trade.Price = price;
                        }
                        int qty;
                        if (int.TryParse(attrs[3], out qty))
                        {
                            trade.Quantity = qty;
                        }
                        trades.Add(trade);
                    }
                }
            }

            return await Task.Run(() => trades);
        }

        private static string[] ParseCsv(string line)
        {
            bool lockTaken = false;
            Monitor.Enter(sync, ref lockTaken);
            TextFieldParser parser = null;
            try
            {
                parser = new TextFieldParser(new StringReader(line));

                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");

                string[] fields = null;

                while (!parser.EndOfData)
                {
                    fields = parser.ReadFields();
                }
                return fields;
            }
            finally
            {
                if (lockTaken)
                {
                    parser.Close();
                    Monitor.Exit(sync);
                }    
            }
        }
    }


}