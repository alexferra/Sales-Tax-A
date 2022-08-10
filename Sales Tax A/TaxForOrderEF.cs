using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// used for Tax Order
/// </summary>
namespace Sales_Tax_A
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class TaxForOrderEF
    {
        public string from_country { get; set; }
        public string from_zip { get; set; }
        public string from_state { get; set; }
        public string to_country { get; set; }
        public string to_zip { get; set; }
        public string to_state { get; set; }
        public double amount { get; set; }
        public double shipping { get; set; }
        public List<LineItem> line_items { get; set; }


        public class LineItem
        {
            public int quantity { get; set; }
            public double unit_price { get; set; }
            public string product_tax_code { get; set; }
        }
    }

}
