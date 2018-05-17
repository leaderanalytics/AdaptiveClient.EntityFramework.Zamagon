using System;
using System.Collections.Generic;
using System.Text;

namespace Zamagon.Model
{
    public class Product
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
