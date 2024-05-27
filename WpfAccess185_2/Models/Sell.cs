using System;
namespace WpfAccess185_2.Models
{
    public class Sell
    {
        public int SellId { set; get; }
        public int GoodId { set; get; }
        public DateTime DateSell { set; get; }
        public int SellCount { set; get; }
        public Sell(int sellId, int goodId, DateTime date, int count)
        {
            SellId = sellId;
            GoodId = goodId;
            DateSell = date;
            SellCount = count;
        }
    }
}
