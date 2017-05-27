using MW.Budgeting.Common.Helper;
using MW.Budgeting.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Model.Helper
{
    /// <summary>
    /// A helper to convert an SQL-DataSet into the requested type
    /// </summary>
    public static class ConversionHelper
    {
        public static List<T> Convert<T>(DataSet ds)
        {
            List<T> items = new List<T>();

            try
            {
                if (typeof(T).FullName == typeof(Account).FullName)
                {
                    foreach (DataRow row in ds.Tables["Account"].Rows)
                    {
                        Account acc = new Account();
                        acc.ID = Guid.Parse(row["ID"].ToString());
                        acc.Name = row["Name"].ToString();
                        acc.Note = row["Note"].ToString();
                        acc.IsOffBudget = bool.Parse(row["IsOffBudget"].ToString());
                        acc.IsOffBudget = bool.Parse(row["IsActive"].ToString());
                        Enums.AccountType type = Enums.AccountType.None;
                        Enum.TryParse<Enums.AccountType>(row["Type"].ToString(), out type);
                        acc.Type = type;
                    }
                }
                else if (typeof(T).FullName == typeof(Category).FullName)
                {

                }
                else if (typeof(T).FullName == typeof(Entry).FullName)
                {

                }
                else if (typeof(T).FullName == typeof(Payee).FullName)
                {

                }
            }
            catch (Exception ex)
            {
                // TODO: Add logging
            }

            return items;
        }
    }
}
