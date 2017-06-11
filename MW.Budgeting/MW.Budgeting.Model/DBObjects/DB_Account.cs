using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.Accounts;
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
    public class DB_Account : IDBObject
    {
        public DB_Account()
        {
            
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsOffBudget { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Load(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            string sql = SQLScripts.GET_SELECTED_ACCOUNT;
            sql = sql.Replace("[ID]", id);
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Account");
            DB_Account acc = ConversionHelper.Convert<DB_Account>(ds).Cast<DB_Account>().FirstOrDefault();

            if (acc != null)
            {
                this.ID = acc.ID;
                this.Name = acc.Name;
                this.Note = acc.Note;
                this.IsOffBudget = acc.IsOffBudget;
                this.IsActive = acc.IsActive;
                this.Type = acc.Type;
            }
        }

        public void LoadFromName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            string sql = SQLScripts.GET_ACCOUNT_FROM_NAME;
            sql = sql.Replace("[NAME]", name);
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Account");
            DB_Account acc = ConversionHelper.Convert<DB_Account>(ds).Cast<DB_Account>().FirstOrDefault();

            if (acc != null)
            {
                this.ID = acc.ID;
                this.Name = acc.Name;
                this.Note = acc.Note;
                this.IsOffBudget = acc.IsOffBudget;
                this.IsActive = acc.IsActive;
                this.Type = acc.Type;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
