using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Model.Accounts
{
    public class Category
    {
        public Category()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsMasterCategory { get; set; }
        public Category Parent { get; set; }
        public List<Category> Childs { get; set; }
    }
}
