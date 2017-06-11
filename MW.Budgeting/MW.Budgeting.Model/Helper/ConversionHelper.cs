using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.Accounts;
using MW.Budgeting.Model.DBObjects;
using MW.Budgeting.Model.Interfaces;
using System;
using System.Collections.Generic;
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

namespace MW.Budgeting.Model.Helper
{
    /// <summary>
    /// A helper to convert an SQL-DataSet into the requested type
    /// </summary>
    public static class ConversionHelper
    {
        public static List<IDBObject> Convert<T>(DataSet ds)
        {
            List<IDBObject> objects = new List<IDBObject>();

            if (typeof(T).FullName == typeof(DB_Account).FullName)
            {
                foreach (DataRow row in ds.Tables["Account"].Rows)
                {
                    DB_Account acc = new DB_Account();
                    acc.ID = row["ID"].ToString();
                    acc.IsActive = bool.Parse(row["IsActive"].ToString());
                    acc.IsOffBudget = bool.Parse(row["IsOffBudget"].ToString());
                    acc.Name = row["Name"].ToString();
                    acc.Note = row["Note"].ToString();
                    acc.Type = row["Type"].ToString();

                    objects.Add(acc);
                }
            }
            else if (typeof(T).FullName == typeof(DB_Entry).FullName)
            {
                foreach (DataRow row in ds.Tables["Entry"].Rows)
                {
                    DB_Entry ent = new DB_Entry();
                    ent.Account = row["Account"].ToString();
                    ent.Category = row["Category"].ToString();
                    ent.Date = row["Date"].ToString();
                    ent.ID = row["ID"].ToString();
                    ent.Inflow = decimal.Parse(row["Inflow"].ToString());
                    ent.Outflow = decimal.Parse(row["Outflow"].ToString());
                    ent.Payee = row["Payee"].ToString();

                    objects.Add(ent);
                }
            }
            else if (typeof(T).FullName == typeof(DB_Category).FullName)
            {
                foreach (DataRow row in ds.Tables["Category"].Rows)
                {
                    DB_Category cat = new DB_Category();
                    cat.ID = row["ID"].ToString();
                    cat.IsMasterCategory = bool.Parse(row["IsMasterCategory"].ToString());
                    cat.Name = row["Name"].ToString();
                    cat.ParentCategory = row["ParentCategory"].ToString();

                    objects.Add(cat);
                }
            }
            else if (typeof(T).FullName == typeof(DB_Payee).FullName)
            {
                foreach (DataRow row in ds.Tables["Payee"].Rows)
                {
                    DB_Payee pay = new DB_Payee();
                    pay.ID = row["ID"].ToString();
                    pay.IsActive = bool.Parse(row["IsActive"].ToString());
                    pay.Name = row["Name"].ToString();

                    objects.Add(pay);
                }
            }

            return objects;
        }
    }
}
