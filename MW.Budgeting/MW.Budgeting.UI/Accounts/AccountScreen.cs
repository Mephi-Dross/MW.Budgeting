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
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            Entries = new List<Entry>();

            this.dgEntries.DataSource = Entries;

            CreateAccount("TEST");
            ChangeAccount("TEST");
        }

        public MainForm MainForm { get; set; }
        public List<Entry> Entries { get; set; }

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
    }
}
