using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.Helper;
using MW.Budgeting.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Model.DBObjects
{
    public class DB_Category : IDBObject
    {
        public DB_Category()
        {

        }

        public string ID { get; set; }
        public string Name { get; set; }
        public bool IsMasterCategory { get; set; }
        public string ParentCategory { get; set; }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Load(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            string sql = SQLScripts.GET_SELECTED_CATEGORY;
            sql = sql.Replace("[ID]", id);
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Category");
            DB_Category cat = ConversionHelper.Convert<DB_Category>(ds).Cast<DB_Category>().FirstOrDefault();

            if (cat != null)
            {
                this.ID = cat.ID;
                this.Name = cat.Name;
                this.IsMasterCategory = cat.IsMasterCategory;
                this.ParentCategory = cat.ParentCategory;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
