using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.Accounts;
using MW.Budgeting.Model.DBObjects;
using MW.Budgeting.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static List<Payee> GetPayees()
        {
            List<Payee> payees = new List<Payee>();

            DataSet ds = SQLHelper.ExecuteDataSet(SQLScripts.GET_ACTIVE_PAYEES, "Payee");
            List<DB_Payee> dbPayees = ConversionHelper.Convert<DB_Payee>(ds).Cast<DB_Payee>().ToList();

            foreach (DB_Payee dbPay in dbPayees)
            {
                Payee pay = new Payee();
                pay.ConvertFromDBObject(dbPay);
                payees.Add(pay);
            }

            ConnectData();
            return payees;
        }

        public static List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            DataSet ds = SQLHelper.ExecuteDataSet(SQLScripts.GET_CATEGORIES, "Category");
            List<DB_Category> dbCategories = ConversionHelper.Convert<DB_Category>(ds).Cast<DB_Category>().ToList();

            foreach (DB_Category dbCat in dbCategories)
            {
                Category cat = new Category();
                cat.ConvertFromDBObject(dbCat);
                categories.Add(cat);
            }

            ConnectData();
            return categories;
        }
    }
}
