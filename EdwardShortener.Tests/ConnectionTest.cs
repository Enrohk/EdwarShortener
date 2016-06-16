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
        [TestMethod]
        public void ConnectionDhouldWorkTest()
        {
            string con = Properties.Settings.Default["EdwardShortenerDbCS"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string query = "SELECT * FROM [dbo].[SQL_Function_ShortedURIListByTime] ()";
                IEnumerable<UrlObject> v_result = myConnection.Query<UrlObject>(query);
                Assert.IsTrue(v_result.ToList().Count > 0);
            }
        }
    }
}
