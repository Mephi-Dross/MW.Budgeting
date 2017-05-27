using MW.Budgeting.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace MW.Budgeting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Creating Database
            if (!Directory.Exists(".\\Data"))
                Directory.CreateDirectory(".\\Data");

            if (!File.Exists(".\\Data\\MW_TEST.db"))
                SQLHelper.CreateDB("MW_TEST");

            string filePath = System.Windows.Forms.Application.StartupPath + "\\Data\\MW_Test.db";
            SQLHelper.Initialize(filePath);
            if (!SQLHelper.TestConnection())
                MessageBox.Show("ERROR DURING CONNECTION!");

            Application.Run(new MW.Budgeting.UI.Main.MainForm());
        }
    }
}
