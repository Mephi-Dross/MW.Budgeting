using MW.Budgeting.Common.Helper;
using MW.Budgeting.Common.SQL;
using MW.Budgeting.Model.DBObjects;
using MW.Budgeting.Model.Helper;
using MW.Budgeting.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MW.Budgeting.Model.Accounts
{
    public class Category : ISaveable, INotifyPropertyChanged, IConnectable
    {
        public Category()
        {
            ID = Guid.NewGuid();
            Childs = new List<Category>();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsMasterCategory { get; set; }
        public Category ParentCategory { get; set; }

        public List<Category> Childs { get; set; }


        public void LoadChildCategories()
        {
            List<DB_Category> dbCats = new List<DB_Category>();
            string sql = SQLScripts.GET_CHILD_CATEGORIES.Replace("[ID]", this.ID.ToString());
            DataSet ds = SQLHelper.ExecuteDataSet(sql, "Category");
            dbCats = ConversionHelper.Convert<DB_Category>(ds).Cast<DB_Category>().ToList();

            this.Childs.Clear();

            foreach (DB_Category dbCat in dbCats)
            {
                Category cat = new Category();
                cat.ConvertFromDBObject(dbCat);
                this.Childs.Add(cat);
            }
        }

        #region ISaveable-Implementation

        public IDBObject ConvertToDBObject()
        {
            DB_Category dbCat = new DB_Category();

            dbCat.ID = this.ID.ToString();
            dbCat.IsMasterCategory = this.IsMasterCategory;
            dbCat.Name = this.Name;
            dbCat.ParentCategory = this.ParentCategory == null ? string.Empty : this.ParentCategory.ID.ToString();

            return dbCat;
        }

        public void ConvertFromDBObject(IDBObject obj)
        {
            if (!(obj is DB_Category))
                return;

            DB_Category dbCat = obj as DB_Category;
            this.ID = Guid.Parse(dbCat.ID);
            this.IsMasterCategory = dbCat.IsMasterCategory;
            this.Name = dbCat.Name;

            DataHelper.AddItem(this);

            if (!string.IsNullOrEmpty(dbCat.ParentCategory))
            {
                DB_Category parentDBCat = new DB_Category();
                parentDBCat.Load(dbCat.ParentCategory);
                ISaveable saveable = DataHelper.LoadedObjects.FirstOrDefault(lo => lo.ID.ToString() == parentDBCat.ID);
                if (saveable != null)
                {
                    Category parentCat = DataHelper.LoadedObjects.Where(lo => lo is Category).Cast<Category>().FirstOrDefault(lo => lo.ID.ToString() == parentDBCat.ID);
                    this.ParentCategory = parentCat;
                }
                else
                {
                    Category parentCat = new Category();
                    parentCat.ConvertFromDBObject(parentDBCat);
                    this.ParentCategory = parentCat;
                }
            }
        }

        #endregion

        #region IConnectable-Implementation

        public void Connect()
        {
            this.LoadChildCategories();
        }

        #endregion

        #region INotifyPropertyChanged-Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
