using MW.Budgeting.Common.Helper;
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
            // Initializing
            this.AccountScreen = new AccountScreen();
            this.BudgetScreen = new BudgetScreen();
            this.NavBar = new NavBar();

            // Setting the parent to this
            this.AccountScreen.MainForm = this;
            this.BudgetScreen.MainForm = this;
            this.NavBar.MainForm = this;

            // Creating the standard screen you see when you open the program. NavBar left, Accounts right.
            this.MainSplitter.Panel1.Controls.Add(this.NavBar);
            this.MainSplitter.Panel2.Controls.Add(this.AccountScreen);

            // Creating Database
            if (!File.Exists("MW_TEST.db"))
                SQLHelper.CreateDB("TEST");
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
