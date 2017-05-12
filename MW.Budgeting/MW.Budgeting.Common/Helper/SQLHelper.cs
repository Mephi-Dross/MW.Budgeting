using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Common.Helper
{
    public static class SQLHelper
    {
        public static void Initialize()
        {
            
        }

        public static void CreateDB(string name)
        {
            SQLiteConnection.CreateFile(string.Format("MW_{0}.db", name));
        }

        public static void Connect()
        {

        }

        public static void Disconnect()
        {

        }

        public static void ExecuteCommand()
        {

        }
    }
}
