using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MW.Budgeting.UI.Main;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.Accounts;
using MW.Budgeting.UI.Controls.Grids;
using MW.Budgeting.Common.Helper;
using MW.Budgeting.Model.Helper;

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

namespace MW.Budgeting.UI.Accounts
{
    public partial class AccountScreen : UserControl
    {
        public AccountScreen(string accName)
        {
            //CreateAccount("TEST");
            //CreatePayees();

            //Category c1 = new Category();
            //c1.Name = "Groceries";
            //c1.IsMasterCategory = true;
            //c1.Save();

            //Category c2 = new Category();
            //c2.Name = "Alcohol";
            //c2.IsMasterCategory = false;
            //c2.Parent = c1;
            //c2.Save();

            InitializeComponent();
            this.Dock = DockStyle.Fill;
            Entries = new BindingList<Entry>();

            // Create the grid columns manually
            this.dgEntries.AutoGenerateColumns = false;
            CreateGridColumns();

            this.dgEntries.DataSource = Entries;
            this.dgEntries.RowValidating += DgEntries_RowValidating;
            this.dgEntries.CellEndEdit += DgEntries_CellEndEdit;

            LoadData(accName);
        }



        public MainForm MainForm { get; set; }
        private BindingList<Entry> Entries;
        private Account currentAccount;


        #region Grid-Events

        private void DgEntries_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEntries[e.ColumnIndex, e.RowIndex] is DataGridViewComboBoxCell)
            { 
                DataGridViewComboBoxCell dcc = (DataGridViewComboBoxCell)dgEntries[e.ColumnIndex, e.RowIndex];
                if (dcc != null && dcc.Value != null)
                {
                    foreach (var item in dcc.Items)
                    {
                        if (item is Category)
                        {
                            Category cat = item as Category;
                            if (dcc.Value.ToString() == cat.Name)
                            {
                                dcc.Tag = cat;
                                break;
                            }
                        }

                        if (item is Payee)
                        {
                            Payee pay = item as Payee;
                            if (dcc.Value.ToString() == pay.Name)
                            {
                                dcc.Tag = pay;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void DgEntries_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgEntries.IsCurrentRowDirty)
            {
                // Save changes to DB
                DataGridViewRow row = dgEntries.Rows[e.RowIndex];
                Entry entry = Entries[e.RowIndex];
                if (row == null || entry == null)
                    return;

                entry.Date = DateTime.Parse(((DateCell)row.Cells["Date"]).Value.ToString());

                entry.Inflow = row.Cells["Inflow"].Value == null ? 0 : decimal.Parse( row.Cells["Inflow"].Value.ToString());
                entry.Outflow = row.Cells["Outflow"].Value == null ? 0 : decimal.Parse(row.Cells["Outflow"].Value.ToString());

                DataGridViewComboBoxCell cbcCat = row.Cells["Category"] as DataGridViewComboBoxCell;
                if (cbcCat != null && cbcCat.Tag is Category)
                    entry.Category = cbcCat.Tag as Category;

                DataGridViewComboBoxCell cbcPay = row.Cells["Payee"] as DataGridViewComboBoxCell;
                if (cbcPay != null && cbcPay.Tag is Payee)
                    entry.Payee = cbcPay.Tag as Payee;

                DataGridViewCheckBoxCell cbcDone = row.Cells["Done?"] as DataGridViewCheckBoxCell;
                if (cbcDone.Value != null)
                    entry.IsDone = bool.Parse(cbcDone.Value.ToString());
                else
                    entry.IsDone = false;

                entry.Account = this.currentAccount;

                try
                {
                    entry.ConvertToDBObject().Save();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        #endregion

        public void CreateGridColumns()
        {
            // ID
            //DataGridViewTextBoxColumn idCol = new DataGridViewTextBoxColumn();
            //idCol.Name = "ID";
            //idCol.Visible = false;

            // Date
            DateColumn dateCol = new DateColumn();
            dateCol.Name = "Date";

            // Account
            DataGridViewTextBoxColumn accCol = new DataGridViewTextBoxColumn();
            accCol.Name = "Account";
            accCol.Visible = false;

            // Payee
            DataGridViewComboBoxColumn payCol = new DataGridViewComboBoxColumn();
            payCol.ValueType = typeof(Payee);
            payCol.DisplayMember = "Name";
            payCol.DataSource = GetPayees();
            payCol.Name = "Payee";

            // Category
            DataGridViewComboBoxColumn catCol = new DataGridViewComboBoxColumn();
            catCol.ValueType = typeof(Category);
            catCol.DisplayMember = "Name";
            catCol.DataSource = GetCategories();
            catCol.Name = "Category";

            // Outflow
            // TODO: Numerical CellTemplate
            DataGridViewTextBoxColumn outCol = new DataGridViewTextBoxColumn();
            outCol.Name = "Outflow";

            // Inflow
            // TODO: Numerical CellTemplate
            DataGridViewTextBoxColumn inCol = new DataGridViewTextBoxColumn();
            inCol.Name = "Inflow";

            // IsDone
            DataGridViewCheckBoxColumn doneCol = new DataGridViewCheckBoxColumn();
            doneCol.Name = "Done?";

            //this.dgEntries.Columns.Add(idCol);
            this.dgEntries.Columns.Add(dateCol);
            this.dgEntries.Columns.Add(payCol);
            this.dgEntries.Columns.Add(catCol);
            this.dgEntries.Columns.Add(outCol);
            this.dgEntries.Columns.Add(inCol);
            this.dgEntries.Columns.Add(doneCol);

            //this.dgEntries.Columns.AddRange(idCol, dateCol, payCol, catCol, outCol, inCol, doneCol);
        }

        public void LoadData(string accName)
        {
            Model.Accounts.Account acc = new Model.Accounts.Account(accName);
            DataHelper.ConnectData();
            this.currentAccount = acc;

            foreach (Entry entry in currentAccount.Entries)
            {
                Entries.Add(entry);
            }

            RefreshData();
        }

        public void RefreshData()
        {
            this.dgEntries.DataSource = Entries;
        }


        #region Temp

        public void ChangeAccount(string accountName)
        {
            Model.Accounts.Account acc = new Model.Accounts.Account(accountName);
            this.currentAccount = acc;

            foreach (Entry entry in currentAccount.Entries)
            {
                Entries.Add(entry);
            }

        }

        public void CreateAccount(string accountName)
        {
            Model.Accounts.Account acc = new Model.Accounts.Account();
            acc.Name = accountName;
            acc.IsActive = true;
            acc.IsOffBudget = false;
            acc.Note = "Testnote";
            acc.Type = Model.Enums.AccountType.Cash;

            //acc.Save();
        }

        private void CreatePayees()
        {
            Payee p1 = new Payee() { IsActive = true, Name = "P1" };
            Payee p2 = new Payee() { IsActive = false, Name = "P2" };
            Payee p3 = new Payee() { IsActive = true, Name = "P3" };
            Payee p4 = new Payee() { IsActive = true, Name = "P4" };

            //p1.Save();
            //p2.Save();
            //p3.Save();
            //p4.Save();
        }

        private List<Payee> GetPayees()
        {
            DataSet ds = SQLHelper.ExecuteDataSet(SQLScripts.GET_ACTIVE_PAYEES, "Payee");
            List<Payee> payees = ConversionHelper.Convert<Payee>(ds).Cast<Payee>().ToList();
            return payees;
        }

        private List<Category> GetCategories()
        {
            DataSet ds = SQLHelper.ExecuteDataSet(SQLScripts.GET_CATEGORIES, "Category");
            List<Category> categories = ConversionHelper.Convert<Category>(ds).Cast<Category>().ToList();
            return categories;
        }


        #endregion
    }
}
