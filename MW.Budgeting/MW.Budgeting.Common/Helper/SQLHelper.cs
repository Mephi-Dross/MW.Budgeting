using MW.Budgeting.Common.SQL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
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

namespace MW.Budgeting.Common.Helper
{
    public static class SQLHelper
    {
        private static string connectionString = "Data Source=[PATH];Version=3;";
        private static SQLiteConnection Connection;

        public static void Initialize(string filePath)
        {
            connectionString = connectionString.Replace("[PATH]", filePath);
            Connection = new SQLiteConnection(connectionString);
        }

        public static void CreateDB(string name)
        {
            SQLiteConnection.CreateFile(string.Format(".\\Data\\{0}.db", name));

            connectionString = connectionString.Replace("[PATH]", string.Format(".\\Data\\{0}.db", name));
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd;
            try
            {
                con.Open();
                cmd = new SQLiteCommand(SQLScripts.CREATE_CATEGORY_TABLE, con);
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand(SQLScripts.CREATE_PAYEE_TABLE, con);
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand(SQLScripts.CREATE_ENTRY_TABLE, con);
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand(SQLScripts.CREATE_ACCOUNT_TABLE, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //TODO: Add Logging
                System.Diagnostics.Debug.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public static bool TestConnection()
        {
            string SQL = @"SELECT * FROM Entry";
            if (ExecuteNonQuery(SQL) == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Executes the given command and returns -1 in case of error
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            int value = 0;
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            try
            {
                Connection.Open();
                value = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //TODO: Add proper logging
                System.Diagnostics.Debug.Write(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return value;
        }

        public static List<NameValueCollection> ExecuteReader(string sql)
        {
            SQLiteDataReader reader;
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            List<NameValueCollection> rows = new List<NameValueCollection>();
            try
            {
                Connection.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    rows.Add(reader.GetValues());
                }
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                System.Diagnostics.Debug.Write(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return rows;
        }

        public static DataSet ExecuteDataSet(string sql, params string[] tableNames)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

            DataSet ds = new DataSet();
            try
            {
                Connection.Open();
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                System.Diagnostics.Debug.Write(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < tableNames.Length; i++)
                {
                    ds.Tables[i].TableName = tableNames[i];
                }
            }

            return ds;
        }
    }
}
