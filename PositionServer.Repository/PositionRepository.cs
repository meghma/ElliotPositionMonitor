using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using CsvHelper;
using PositionServer.Interfaces;
using PositionServer.Entities;


namespace PositionServer.Repository
{
    public class PositionRepository : IPositionRepository
    {
        public PositionRepository()
        {

        }
        public async Task<IEnumerable<Position>> GetPositionsAsync()
        {
            IEnumerable<Security> securities = null;
            IEnumerable<Trade> trades = null;
            var tasks = new List<Task>
                {
                    Task.Run(async () => securities = await GetSecuritiesAsync()),

                    Task.Run(async () => trades = await GetTradesAsync())
                };

            await Task.WhenAll(tasks);

            var positions = from t in trades
                            join s in securities
                            on t.SecurityID equals s.ID
                            select new
                            {
                                t,
                                s
                            } into p
                            group p by p.t.SecurityID into g
                            select new Position
                            {
                                SecurityID = g.Key,
                                SecurityName = g.FirstOrDefault().s.Name,
                                AveragePrice = g.Average(x => x.t.Price),
                                NumberOfTrades = g.Count(),
                            };

            return positions;
        }

        public async Task<IEnumerable<Trade>> GetTradesAsync()
        {
            IEnumerable<Security> securities = await GetSecuritiesAsync();
            string fileText;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trades.csv");
            using (var reader = File.OpenText(path))
            {
                fileText = await reader.ReadToEndAsync();
            }
            var trades = ParseCsv<Trade>(fileText);

            var result = trades.Join
                (securities, t => t.SecurityID, s => s.ID, (t, s) =>
                    new TradeWrapper
                    {
                        Date = t.Date,
                        SecurityID = t.SecurityID,
                        Price = t.Price,
                        Quantity = t.Quantity,
                        SecurityName = s.Name
                    }
                );
            return result;
        }


        public async Task<IEnumerable<Security>> GetSecuritiesAsync()
        {
            string fileText;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "securities.csv");
            //string path = "securities.csv";
            using (var reader = File.OpenText(path))
            {
                fileText = await reader.ReadToEndAsync();
            }
            return ParseCsv<Security>(fileText);
        }


        private IEnumerable<T> ParseCsv<T>(string fileText)
        {
            CsvReader csvFile = new CsvReader(new StringReader(fileText));
            csvFile.Configuration.HasHeaderRecord = true;
            csvFile.Read();
            return csvFile.GetRecords<T>().ToList();
        }
    }

}
