using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Model.Interfaces
{
    public interface IDBObject
    {
        // We want to be able to save our changes on the object into the database at any point. So they get a save function.
        void Save();
        // And we want to load it... I did it via the constructor for the account, but I'd rather put it in here. We want to load objects by their ID.
        void Load(string id);
        // Now, what else do we want to do with it? Save will handle Insert and Update statements, Load will handle Select. Ah, of course! Deleting it.
        void Delete();
    }
}
