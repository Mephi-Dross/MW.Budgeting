using MW.Budgeting.Common.Helper;
using MW.Budgeting.Model.Accounts;
using MW.Budgeting.Model.Enums;
using MW.Budgeting.UI.Accounts;
using MW.Budgeting.UI.Budget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

namespace MW.Budgeting.UI.Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Init();
        }

        public NavBar NavBar { get; set; }
        public AccountScreen AccountScreen { get; set; }
        public BudgetScreen BudgetScreen { get; set; }
        public UserControl ActiveScreen { get; set; }   // Even needed? Probably not, but just in case for now...

        public void Init()
        {
            // Fixing Panel sizes
            //this.MainSplitter.Panel2.AutoSize = true;
            //this.MainSplitter.Panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Initializing
            this.AccountScreen = new AccountScreen("TEST");
            this.BudgetScreen = new BudgetScreen();
            this.NavBar = new NavBar();

            // Setting the parent to this
            this.AccountScreen.MainForm = this;
            this.BudgetScreen.MainForm = this;
            this.NavBar.MainForm = this;

            // Creating the standard screen you see when you open the program. NavBar left, Accounts right.
            this.MainSplitter.Panel1.Controls.Add(this.NavBar);
            this.MainSplitter.Panel2.Controls.Add(this.AccountScreen);
        }

        public void SwitchContent(NavItems? item)
        {
            // See what I did up there? The ? marks an object as NULLABLE. What does that mean? That means even if it is an int 
            // it can be null. Normally, ints are 0, never null. So by making it nullable I can give null I can make sure that it won't get a wrong value by accident and do nothing.
            // Not really necessary, but it's a cool trick that I like. The other option would be to have the enum have a value for "Undefined" that just gets ignored.
            if (!item.HasValue)
                return;

            // Remove everything from right side
            this.MainSplitter.Panel2.Controls.Clear();
            // Add new right side
            switch (item)
            {
                case NavItems.Budget:
                    this.MainSplitter.Panel2.Controls.Add(this.BudgetScreen);
                    break;
                case NavItems.Accounts:
                    this.MainSplitter.Panel2.Controls.Add(this.AccountScreen);
                    break;
            }
            
        }
    }
}
