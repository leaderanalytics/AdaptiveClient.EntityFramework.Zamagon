using System;
using System.Collections.Generic;
using System.Text;

namespace Zamagon.Model
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TimeCard> TimeCards { get; set; }
    }
}
