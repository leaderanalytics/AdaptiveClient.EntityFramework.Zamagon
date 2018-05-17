using System;
using System.Collections.Generic;
using System.Text;

namespace Zamagon.Model
{
    public class Order
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
