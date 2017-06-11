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
    public class DB_Payee : IDBObject
    {
        public DB_Payee()
        {

        }

        public string ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }


        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Load(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            string sql = SQLScripts.GET_SELECTED_PAYEE;
            sql = sql.Replace("[ID]", id);
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Payee");
            DB_Payee pay = ConversionHelper.Convert<DB_Payee>(ds).Cast<DB_Payee>().FirstOrDefault();

            if (pay != null)
            {
                this.ID = pay.ID;
                this.Name = pay.Name;
                this.IsActive = pay.IsActive;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
