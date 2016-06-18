using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EdwardShortener.Functions
{
    public class DBConnector
    {
        private string _connectionString;
        public DBConnector ()
        {
            _connectionString = Properties.Settings.Default["EdwardShortenerDbCS"].ToString();
        }

        public List<T> getListItem<T> (string query, object param)
        {
            List<T> list = new List<T>();

            using (DbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                IEnumerable<T> result = connection.Query<T>(query, param);
                list = result.ToList();

            }
            return list;
        } 

    }
}