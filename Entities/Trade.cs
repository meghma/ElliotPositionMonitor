using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Trade
    {
        public DateTime TradeDate { get; set; }
        public int SecurityID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
