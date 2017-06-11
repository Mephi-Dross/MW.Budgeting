using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.DBObjects;
using MW.Budgeting.Model.Enums;
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
    public class Account : ISaveable, INotifyPropertyChanged, IConnectable
    {
        public Account()
        {
            ID = Guid.NewGuid();
            Entries = new List<Entry>();
        }

        public Account(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            Entries = new List<Entry>();

            DB_Account dbAcc = new DB_Account();
            dbAcc.LoadFromName(name);
            this.ConvertFromDBObject(dbAcc);
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
            List<DB_Entry> dbEntries = new List<DB_Entry>();
            string sql = SQLScripts.GET_ENTRIES_FROM_ACCOUNT.Replace("[ACCOUNT]", this.ID.ToString());
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Entry");
            dbEntries = ConversionHelper.Convert<DB_Entry>(ds).Cast<DB_Entry>().ToList();

            Entries.Clear();

            foreach (DB_Entry ent in dbEntries)
            {
                Entry entry = new Entry();
                entry.ConvertFromDBObject(ent);
                Entries.Add(entry);
            }
        }

        #region ISaveable-Functions

        public IDBObject ConvertToDBObject()
        {
            DB_Account acc = new DB_Account();
            acc.ID = this.ID.ToString();
            acc.IsActive = this.IsActive;
            acc.IsOffBudget = this.IsOffBudget;
            acc.Name = this.Name;
            acc.Note = this.Note;
            acc.Type = this.Type.ToString();
            return acc;
        }

        public void ConvertFromDBObject(IDBObject obj)
        {
            if (!(obj is DB_Account))
                return;

            DB_Account acc = obj as DB_Account;
            this.ID = Guid.Parse(acc.ID);
            this.IsActive = acc.IsActive;
            this.IsOffBudget = acc.IsOffBudget;
            this.Name = acc.Name;
            this.Note = acc.Note;
            Enums.AccountType type = Enums.AccountType.None;
            Enum.TryParse<Enums.AccountType>(acc.Type, out type);
            this.Type = type;

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
