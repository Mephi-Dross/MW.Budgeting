using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Model.Interfaces
{
    public interface ISaveable
    {
        Guid ID { get; set; }
        IDBObject ConvertToDBObject();
        void ConvertFromDBObject(IDBObject obj);
    }
}
