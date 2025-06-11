using AutomationFramework.Base;
using AutomationFramework.Pages;
using AutomationFramework.Reports;
using AutomationFramework.Utilities;
using AutomationLibrary;
using AventStack.ExtentReports;
using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {

        [Test]
        public void ValidTestData()
        {
            var data = new ExcelHelper(@"W:/TestAutomation/AutomationDriver_UAT.xlsx", "EventInfoNew");
            var rows = data.GetRowsByDataset("15");
            foreach (var row in rows)
            {
                string data1 = data.GetValue(row, "ActionValues", "");
                string data2 = data.GetValue(row, "ActionValues2", "");
                Console.WriteLine(data1);
                Console.WriteLine($"Not Exist {data2}");
            }
        }

        [Test]
        public void ValidLoginTest1()
        {
            Driver.Navigate().GoToUrl("https://www.google.com/");
            var loginPage = new LoginPage(Driver);

            ReportManager.LogStep(Status.Info, "test message", Driver, true);
            ReportManager.LogStep(Status.Pass, "test message", Driver, true);
            ReportManager.LogStep(Status.Warning, "test message", Driver, true);
            ReportManager.LogStep(Status.Error, "test message", Driver, true);
            ReportManager.LogStep(Status.Fail, "test message", Driver, true);
            ReportManager.LogStep(Status.Skip, "test message", Driver, true);
            //loginPage.Login("admin", "password");
            //Assert.IsTrue(Driver.Url.Contains("dashboard"));
        }

        [Test]
        public void DatabaseConection()
        {
            string oracleConnStr = DBHelper.BuildOracleConnectionString("host", "1521", "service", "user", "pass");
            string sqlConnStr = DBHelper.BuildSqlServerConnectionString("server", "database", "user", "pass");

            // Oracle example:
            using (var db = new DBHelper(DatabaseType.Oracle, oracleConnStr))
            {
                db.Open();
                var dt = db.ExecuteQuery("SELECT * FROM employees");
                Console.WriteLine($"Rows: {dt.Rows.Count}");
            }

            // SQL Server example:
            using (var db = new DBHelper(DatabaseType.SqlServer, sqlConnStr))
            {
                db.Open();
                var result = db.ExecuteScalar("SELECT COUNT(*) FROM customers");
                Console.WriteLine($"Customers: {result}");
            } //Automatically close conection

            // SQL Table Server example:
            using (var db = new DBHelper(DatabaseType.SqlServer, sqlConnStr))
            {
                db.Open();
                DataTable dt = db.ExecuteQuery("SELECT * FROM employees");

                //Read specific column
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["EmployeeID"].ToString();
                    string name = row["EmployeeName"].ToString();
                    Console.WriteLine($"ID: {id}, Name: {name}");

                    var value = row["ColumnName"] != DBNull.Value ? row["ColumnName"].ToString() : "N/A";
                    int age = row["Age"] != DBNull.Value ? Convert.ToInt32(row["Age"]) : 0;
                }

                //Read all column as list by row
                foreach (DataRow row in dt.Rows)
                {
                    var values = row.ItemArray; // Array all values from row
                    Console.WriteLine(string.Join(" | ", values));
                }


            }



        }


    }

}
