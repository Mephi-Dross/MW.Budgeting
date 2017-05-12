using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Model.Accounts
{
    public class Payee
    {
        public Payee()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<Entry> Entries { get; set; }
        public bool IsActive { get; set; }
    }
}
