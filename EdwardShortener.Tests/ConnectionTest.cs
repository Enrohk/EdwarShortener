using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EdwardShortener.Objects;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using Dapper;

namespace EdwardShortener.Tests
{
    [TestClass]
    public class ConnectionTest
    {
        private string con = Properties.Settings.Default["EdwardShortenerDbCS"].ToString();
        [TestMethod]
        public void ConnectionDhouldWorkTest()
        {
            
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string query = "SELECT * FROM [dbo].[SQL_Function_ShortedURIListByTime] ()";
                IEnumerable<UrlObject> v_result = myConnection.Query<UrlObject>(query);
                Assert.IsTrue(v_result.ToList().Count > 0);
                myConnection.Close();
            }
        }

        [TestMethod]
        public void addNewUrlShouldWork ()
        {
            UrlObject urlToInsert = new UrlObject();
            urlToInsert.shortedUrl = "wwww.google.com";
            urlToInsert.longUrl = "wwww.google.com";

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string query = Properties.Resources.SQL_INSERT_SHORTED_URL;
                int rowsAfected =  myConnection.Execute(query, urlToInsert);
                Assert.IsTrue(rowsAfected > 0);
                myConnection.Close();
            }
        }
    }
}
