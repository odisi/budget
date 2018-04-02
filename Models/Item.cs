using System;

namespace budget.Models
{
    public class Item {
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public String Category { get; set; }
        public ItemType Type { get; set; }
        public Decimal Amount { get; set; }
    }
}