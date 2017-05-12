using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MW.Budgeting.Model.Enums;

namespace MW.Budgeting.UI.Main
{
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
        }

        public MainForm MainForm { get; set; }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            MainForm.SwitchContent(NavItems.Budget);
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            MainForm.SwitchContent(NavItems.Accounts);
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            MainForm.SwitchContent(null);
        }
    }
}
