using MW.Budgeting.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Model.Helper
{
    public static class DataHelper
    {
        public static List<ISaveable> LoadedObjects = new List<ISaveable>();

        public static void ConnectData()
        {
            int count = LoadedObjects.Count;
            List<IConnectable> items = LoadedObjects.Cast<IConnectable>().ToList();
            foreach (IConnectable item in items)
            {
                item.Connect();
            }

            if (count != LoadedObjects.Count)
                ConnectData();
        }

        public static void AddItem(ISaveable item)
        {
            if (LoadedObjects.Contains(item) || LoadedObjects.FirstOrDefault(lo => lo.ID.ToString() == item.ID.ToString()) != null)
                return;
            else
                LoadedObjects.Add(item);
        }
    }
}
