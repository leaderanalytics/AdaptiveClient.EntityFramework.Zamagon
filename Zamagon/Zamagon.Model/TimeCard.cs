using System;
using System.Collections.Generic;
using System.Text;

namespace Zamagon.Model
{
    public class TimeCard
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime WorkDate { get; set; }
        public decimal HoursWorked { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
