using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.Enums;
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

namespace MW.Budgeting.Model.Accounts
{
    public class Account : IDBObject
    {
        public Account()
        {
            ID = Guid.NewGuid();
        }

        public Account(string name)
        {
            Load(name);
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsOffBudget { get; set; }
        public bool IsActive { get; set; }
        public List<Entry> Entries { get; set; }
        public AccountType Type { get; set; }

        public void LoadEntries()
        {

        }

        #region IDBObject-Functions

        public void Save()
        {
            string sql = SQLScripts.INSERT_ACCOUNT;
            sql = sql.Replace("[ID]", this.ID.ToString());
            sql = sql.Replace("[NAME]", this.Name);
            sql = sql.Replace("[NOTE]", this.Note);
            sql = sql.Replace("[ISOFFBUDGET]", this.IsOffBudget.ToString());
            sql = sql.Replace("[ISACTIVE]", this.IsActive.ToString());
            sql = sql.Replace("[TYPE]", this.Type.ToString());
            SQLHelper.ExecuteNonQuery(sql);
        }

        public void Load(string name)
        {
            string sql = SQLScripts.GET_SELECTED_ACCOUNT;
            sql = sql.Replace("[NAME]", name);
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Account");
            Account acc = ConversionHelper.Convert<Account>(ds).FirstOrDefault();
            
            if (acc != null)
            {
                this.ID = acc.ID;
                this.Name = acc.Name;
                this.Note = acc.Note;
                this.IsOffBudget = acc.IsOffBudget;
                this.IsActive = acc.IsActive;
                this.Type = acc.Type;
                this.LoadEntries();
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        #endregion
    } 
}
