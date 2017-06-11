using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.DBObjects;
using MW.Budgeting.Model.Helper;
using MW.Budgeting.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class Payee : ISaveable, INotifyPropertyChanged, IConnectable
    {
        public Payee()
        {
            ID = Guid.NewGuid();
            Entries = new List<Entry>();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<Entry> Entries { get; set; }
        public bool IsActive { get; set; }

        public void LoadEntries()
        {
            List<DB_Entry> dbEntries = new List<DB_Entry>();
            string sql = SQLScripts.GET_PAYEE_ENTRIES.Replace("[ID]", this.ID.ToString());
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Entry");
            dbEntries = ConversionHelper.Convert<DB_Entry>(ds).Cast<DB_Entry>().ToList();

            this.Entries.Clear();

            foreach (DB_Entry ent in dbEntries)
            {
                Entry entry = new Entry();
                entry.ConvertFromDBObject(ent);
                this.Entries.Add(entry);
            }
        }

        #region ISaveable-Implementation

        public IDBObject ConvertToDBObject()
        {
            DB_Payee dbPay = new DB_Payee();

            dbPay.ID = this.ID.ToString();
            dbPay.IsActive = this.IsActive;
            dbPay.Name = this.Name;

            return dbPay;
        }

        public void ConvertFromDBObject(IDBObject obj)
        {
            if (!(obj is DB_Payee))
                return;

            DB_Payee dbPay = obj as DB_Payee;
            this.ID = Guid.Parse(dbPay.ID);
            this.IsActive = dbPay.IsActive;
            this.Name = dbPay.Name;

            DataHelper.AddItem(this);
        }

        #endregion

        #region IConnectable-Implementation

        public void Connect()
        {
            this.LoadEntries();
        }

        #endregion

        #region INotifyPropertyChanged-Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
