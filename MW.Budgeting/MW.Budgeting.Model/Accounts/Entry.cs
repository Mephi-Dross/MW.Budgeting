using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Model.Accounts
{
    public class Entry
    {
        public Entry()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public Payee Payee { get; set; }
        public Category Category { get; set; }
        public decimal Outflow { get; set; }
        public decimal Inflow { get; set; }
        public bool IsDone { get; set; }
    }
}
