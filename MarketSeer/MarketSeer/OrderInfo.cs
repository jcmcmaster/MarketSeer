using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSeer
{
    public class OrderInfoMessage
    {
        public OrderInfo[] OrderInfoArr { get; set; }
    }

    public class OrderInfo
    {
        public long order_id { get; set; }
        public int type_id { get; set; }
        public long location_id { get; set; }
        public int volume_total { get; set; }
        public int volume_remain { get; set; }
        public int min_volume { get; set; }
        public float price { get; set; }
        public bool is_buy_order { get; set; }
        public int duration { get; set; }
        public DateTime issued { get; set; }
        public string range { get; set; }
    }
}
