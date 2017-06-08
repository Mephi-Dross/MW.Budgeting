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
        public AccountScreen()
        {
            //CreateAccount("TEST");
            ChangeAccount("TEST");
            //CreatePayees();


            InitializeComponent();
            this.Dock = DockStyle.Fill;
            Entries = new BindingList<Entry>();

            this.dgEntries.AutoGenerateColumns = false;
            this.dgEntries.DataSource = Entries;

            // Create the grid columns manually
            CreateGridColumns();



            this.dgEntries.DataError += DgEntries_DataError;
        }

        private void DgEntries_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {

            MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
        }

        public MainForm MainForm { get; set; }
        public BindingList<Entry> Entries { get; set; }

        public void CreateGridColumns()
        {
            // ID
            DataGridViewTextBoxColumn idCol = new DataGridViewTextBoxColumn();
            idCol.Name = "ID";
            idCol.Visible = false;

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
            //catCol.Items.AddRange(GetPayees());
            catCol.Name = "Category";

            // Outflow
            // TODO: Numerical CellTemplate
            DataGridViewTextBoxColumn outCol = new DataGridViewTextBoxColumn();
            outCol.Name = "Outflow";

            // Inflow
            // TODO: Numerical CellTemplate
            DataGridViewTextBoxColumn inCol = new DataGridViewTextBoxColumn();
            outCol.Name = "Inflow";

            // IsDone
            DataGridViewCheckBoxColumn doneCol = new DataGridViewCheckBoxColumn();
            doneCol.Name = "Done?";

            this.dgEntries.Columns.Add(idCol);
            this.dgEntries.Columns.Add(dateCol);
            this.dgEntries.Columns.Add(payCol);
            this.dgEntries.Columns.Add(catCol);
            this.dgEntries.Columns.Add(outCol);
            this.dgEntries.Columns.Add(inCol);
            this.dgEntries.Columns.Add(doneCol);

            //this.dgEntries.Columns.AddRange(idCol, dateCol, payCol, catCol, outCol, inCol, doneCol);
        }


        public void ChangeAccount(string accountName)
        {
            Model.Accounts.Account acc = new Model.Accounts.Account(accountName);
        }

        public void CreateAccount(string accountName)
        {
            Model.Accounts.Account acc = new Model.Accounts.Account();
            acc.Name = accountName;
            acc.IsActive = true;
            acc.IsOffBudget = false;
            acc.Note = "Testnote";
            acc.Type = Model.Enums.AccountType.Cash;

            acc.Save();
        }

        private void CreatePayees()
        {
            Payee p1 = new Payee() { IsActive = true, Name = "P1" };
            Payee p2 = new Payee() { IsActive = false, Name = "P2" };
            Payee p3 = new Payee() { IsActive = true, Name = "P3" };
            Payee p4 = new Payee() { IsActive = true, Name = "P4" };

            p1.Save();
            p2.Save();
            p3.Save();
            p4.Save();
        }

        private List<Payee> GetPayees()
        {
            DataSet ds = SQLHelper.ExecuteDataSet(SQLScripts.GET_ACTIVE_PAYEES, "Payee");
            List<Payee> payees = ConversionHelper.Convert<Payee>(ds).Cast<Payee>().ToList();
            return payees;
        }
    }
}
