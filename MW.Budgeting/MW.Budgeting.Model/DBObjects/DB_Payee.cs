using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.Helper;
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

namespace MW.Budgeting.Model.DBObjects
{
    public class DB_Payee : IDBObject
    {
        public DB_Payee()
        {

        }

        public string ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }


        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Load(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            string sql = SQLScripts.GET_SELECTED_PAYEE;
            sql = sql.Replace("[ID]", id);
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Payee");
            DB_Payee pay = ConversionHelper.Convert<DB_Payee>(ds).Cast<DB_Payee>().FirstOrDefault();

            if (pay != null)
            {
                this.ID = pay.ID;
                this.Name = pay.Name;
                this.IsActive = pay.IsActive;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
