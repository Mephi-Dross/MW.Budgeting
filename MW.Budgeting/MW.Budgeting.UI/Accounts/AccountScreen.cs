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
            this.dgEntries.DataSource = Entries;

            // Rebind the grids cell editors to more useful items
            this.dgEntries.Columns["ID"].Visible = false;
            this.dgEntries.Columns["Account"].Visible = false;

            this.dgEntries.Columns["Date"].CellTemplate = new DateCell();

            // TODO: Show these two columns as ComboBoxes with all the required values
            this.dgEntries.Columns["Payee"].CellTemplate = new ComboCell<Payee>();
            this.dgEntries.Columns["Category"].CellTemplate = new ComboCell<Category>();

            this.dgEntries.EditingControlShowing += DgEntries_EditingControlShowing;
        }

        private void DgEntries_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboEditor<Payee>)
            {
                ComboEditor<Payee> editor = e.Control as ComboEditor<Payee>;
                foreach (Payee payee in GetPayees())
                {
                    editor.AddValue(payee);
                }
            }
        }

        public MainForm MainForm { get; set; }
        public BindingList<Entry> Entries { get; set; }


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
