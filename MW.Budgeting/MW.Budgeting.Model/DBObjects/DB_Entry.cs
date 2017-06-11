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

/*  A replacement for the YNAB4 windows application, should it ever be retired.
 *  See License.txt for the full license.
 *  Copyright (C) 2017 Mephi-Dross
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace MW.Budgeting.Model.DBObjects
{
    public class DB_Entry : IDBObject
    {
        public DB_Entry()
        {

        }

        public string ID { get; set; }
        public string Date { get; set; }
        public string Account { get; set; }
        public string Payee { get; set; }
        public string Category { get; set; }
        public decimal Outflow { get; set; }
        public decimal Inflow { get; set; }
        public bool IsDone { get; set; }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Load(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            string sql = SQLScripts.GET_SELECTED_ENTRY;
            sql = sql.Replace("[ID]", id);
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Entry");
            DB_Entry ent = ConversionHelper.Convert<DB_Entry>(ds).Cast<DB_Entry>().FirstOrDefault();

            if (ent != null)
            {
                this.ID = ent.ID;
                this.Date = ent.Date;
                this.Account = ent.Account;
                this.Payee = ent.Payee;
                this.Category = ent.Category;
                this.Outflow = ent.Outflow;
                this.Inflow = ent.Inflow;
                this.IsDone = ent.IsDone;
            }
        }

        public void Save()
        {
            string sql = SQLScripts.GET_SELECTED_ENTRY;
            sql = sql.Replace("[ID]", this.ID);
            var item = SQLHelper.ExecuteScalar(sql);
            if (item == null)
            {
                // Insert
                sql = SQLScripts.INSERT_ENTRY;
                sql = sql.Replace("[ID]", this.ID);
                sql = sql.Replace("[DATE]", this.Date);
                sql = sql.Replace("[ACCOUNT]", this.Account);
                sql = sql.Replace("[PAYEE]", this.Payee);
                sql = sql.Replace("[CATEGORY]", this.Category);
                sql = sql.Replace("[OUTFLOW]", this.Outflow.ToString());
                sql = sql.Replace("[INFLOW]", this.Inflow.ToString());
                sql = sql.Replace("[ISDONE]", this.IsDone.ToString());
                SQLHelper.ExecuteNonQuery(sql);
            }
            else
            {
                // Update
                sql = SQLScripts.UPDATE_ENTRY;
                sql = sql.Replace("[ID]", this.ID);
                sql = sql.Replace("[DATE]", this.Date);
                sql = sql.Replace("[ACCOUNT]", this.Account);
                sql = sql.Replace("[PAYEE]", this.Payee);
                sql = sql.Replace("[CATEGORY]", this.Category);
                sql = sql.Replace("[OUTFLOW]", this.Outflow.ToString());
                sql = sql.Replace("[INFLOW]", this.Inflow.ToString());
                sql = sql.Replace("[ISDONE]", this.IsDone.ToString());
                SQLHelper.ExecuteNonQuery(sql);
            }

        }
    }
}
