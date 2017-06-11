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
    public class Entry : ISaveable, INotifyPropertyChanged, IConnectable
    {
        public Entry()
        {
            ID = Guid.NewGuid();
        }

        private Guid id;
        public Guid ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private Account account;
        public Account Account
        {
            get { return account; }
            set
            {
                account = value;
                OnPropertyChanged("Account");
            }
        }

        private Payee payee;
        public Payee Payee
        {
            get { return payee; }
            set
            {
                payee = value;
                OnPropertyChanged("Payee");
            }
        }

        private Category category;
        public Category Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }

        private decimal outflow;
        public decimal Outflow
        {
            get { return outflow; }
            set
            {
                outflow = value;
                OnPropertyChanged("Outflow");
            }
        }

        private decimal inflow;
        public decimal Inflow
        {
            get { return inflow; }
            set
            {
                inflow = value;
                OnPropertyChanged("Inflow");
            }
        }

        private bool isDone;
        public bool IsDone
        {
            get { return isDone; }
            set
            {
                isDone = value;
                OnPropertyChanged("IsDone");
            }
        }


        #region ISaveable-Implementation

        public IDBObject ConvertToDBObject()
        {
            DB_Entry ent = new DB_Entry();

            ent.Account = this.Account.ID.ToString(); // Entry should always have an account associated with it. If it doesn't, then an error here is fine.
            ent.Category = this.Category.ID.ToString(); // Category is required, so it should throw an error if not existant.
            ent.Date = this.Date.ToString("yyyy-MM-ddTHH:mm:ss");
            ent.ID = this.ID.ToString();
            ent.Inflow = this.Inflow;
            ent.Outflow = this.Outflow;
            ent.Payee = this.Payee == null ? string.Empty : this.Payee.ID.ToString(); // If the payee is null, that's fine. Payee is an optional value.

            return ent;
        }

        public void ConvertFromDBObject(IDBObject obj)
        {
            if (!(obj is DB_Entry))
                return;

            DB_Entry ent = obj as DB_Entry;
            this.Date = DateTime.Parse(ent.Date);
            this.ID = Guid.Parse(ent.ID);
            this.Inflow = ent.Inflow;
            this.Outflow = ent.Outflow;

            DataHelper.AddItem(this);

            if (!string.IsNullOrEmpty(ent.Account))
            {
                DB_Account dbAcc = new DB_Account();
                dbAcc.Load(ent.Account);
                ISaveable saveable = DataHelper.LoadedObjects.FirstOrDefault(lo => lo.ID.ToString() == dbAcc.ID);
                if (saveable != null)
                {
                    Account acc = DataHelper.LoadedObjects.Where(lo => lo is Account).Cast<Account>().FirstOrDefault(lo => lo.ID.ToString() == dbAcc.ID);
                    this.Account = acc;
                }
                else
                {
                    Account acc = new Account();
                    acc.ConvertFromDBObject(dbAcc);
                    this.Account = acc;
                }
            }

            if (!string.IsNullOrEmpty(ent.Category))
            {
                DB_Category dbCat = new DB_Category();
                dbCat.Load(ent.Category);
                ISaveable saveable = DataHelper.LoadedObjects.FirstOrDefault(lo => lo.ID.ToString() == dbCat.ID);
                if (saveable != null)
                {
                    Category cat = DataHelper.LoadedObjects.Where(lo => lo is Category).Cast<Category>().FirstOrDefault(lo => lo.ID.ToString() == dbCat.ID);
                    this.Category = cat;
                }
                else
                {
                    Category cat = new Category();
                    cat.ConvertFromDBObject(dbCat);
                    this.Category = cat;
                }
            }

            if (!string.IsNullOrEmpty(ent.Payee))
            {
                DB_Payee dbPay = new DB_Payee();
                dbPay.Load(ent.Payee);
                ISaveable saveable = DataHelper.LoadedObjects.FirstOrDefault(lo => lo.ID.ToString() == dbPay.ID);
                if (saveable != null)
                {
                    Payee pay = DataHelper.LoadedObjects.Where(lo => lo is Payee).Cast<Payee>().FirstOrDefault(lo => lo.ID.ToString() == dbPay.ID);
                    this.Payee = pay;
                }
                else
                {
                    Payee pay = new Payee();
                    pay.ConvertFromDBObject(dbPay);
                    this.Payee = pay;
                }
            }
        }

        #endregion

        #region IConnectable-Implementation

        public void Connect()
        {
            
        }

        #endregion

        #region INotifyPropertyChanged-Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }
}
