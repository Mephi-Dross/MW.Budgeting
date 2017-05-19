using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Budgeting.Common.SQL
{
    public static class SQLScripts
    {
        public const string CREATE_ENTRY_TABLE =
            @"CREATE TABLE Entry (
                ID varchar(32) NOT NULL,
                Date DateTime,
                Outflow decimal,
                Inflow decimal,
                IsDone bool,
                PRIMARY KEY (ID)
            );";

        public const string CREATE_CATEGORY_TABLE =
            @"CREATE TABLE Category (
                ID VARCHAR(32) PRIMARY KEY  NOT NULL  UNIQUE , 
                Name VARCHAR(255), 
                IsMasterCategory BOOL, 
                ParentCategory VARCHAR(32)
            )";

        public const string CREATE_PAYEE_TABLE = @"CREATE TABLE Payee (ID VARCHAR(32) PRIMARY KEY  NOT NULL  UNIQUE , Name VARCHAR(255), IsActive BOOL, Entry VARCHAR(32))";

        public const string CREATE_ACCOUNT_TABLE = @"CREATE TABLE Account (ID VARCHAR(32) PRIMARY KEY  NOT NULL  UNIQUE , Name VARCHAR(255), Note VARCHAR(255), IsOffBudget BOOL, IsActive BOOL, Entry VARCHAR(32), Type VARCHAR(255))";
    }
}
