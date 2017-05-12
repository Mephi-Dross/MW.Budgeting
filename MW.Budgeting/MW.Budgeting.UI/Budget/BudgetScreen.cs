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

namespace MW.Budgeting.UI.Budget
{
    public partial class BudgetScreen : UserControl
    {
        public BudgetScreen()
        {
            InitializeComponent();
        }

        public MainForm MainForm { get; set; }
    }
}
