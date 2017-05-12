using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Model.Accounts
{
    public class Account
    {
        public Account()
        {
            ID = new Guid();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsOffBudget { get; set; }
        public bool IsActive { get; set; }
        public List<Entry> Entries { get; set; }
        public AccountType Type { get; set; }
    } 
    
    public enum AccountType
    {
        Checking,
        Cash,
        Saving,
        Investment
    }
}
