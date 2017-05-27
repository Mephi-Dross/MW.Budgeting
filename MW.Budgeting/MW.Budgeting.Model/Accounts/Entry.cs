using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.Interfaces;
using System;
using System.Collections.Generic;
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
    public class Entry : IDBObject
    {
        public Entry()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }
        public Payee Payee { get; set; }
        public Category Category { get; set; }
        public decimal Outflow { get; set; }
        public decimal Inflow { get; set; }
        public bool IsDone { get; set; }

        #region IDBObject-Functions

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Load(string id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            string sql = SQLScripts.INSERT_ENTRY;
            sql = sql.Replace("[ID]", this.ID.ToString());
            sql = sql.Replace("[DATE]", Date.ToString("YYYY-MM-ddTHH:mm:ss"));
            sql = sql.Replace("[ACCOUNT]", this.Account.ID.ToString()); // Entry should always have an account associated with it. If it doesn't, then an error here is fine.
            sql = sql.Replace("[PAYEE]", this.Payee == null ? string.Empty : this.Payee.ID.ToString()); // If the payee is null, that's fine. Payee is an optional value.
            sql = sql.Replace("[CATEGORY]", this.Category.ID.ToString()); // Category is required, so it should throw an error if not existant.
            sql = sql.Replace("[OUTFLOW]", this.Outflow.ToString());
            sql = sql.Replace("[INFLOW]", this.Inflow.ToString());
            sql = sql.Replace("[ISDONE]", this.IsDone.ToString());
            SQLHelper.ExecuteNonQuery(sql);
        }

        #endregion
    }
}
