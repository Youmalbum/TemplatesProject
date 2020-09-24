using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace NR.Core.Data
{
    public class DataBase
    {
        private string _strConexion;

        public DataBase()
        {
            _strConexion = "Server=SERVER\\SQLEXPRESS;Database=ventasdb;Trusted_Connection=True";
        }

        public DataBase(string cadenaStrConexion)
        {
            _strConexion = cadenaStrConexion;
        }

        public string StringConexion
        {
            get { return _strConexion;  }
            set { _strConexion = value; }
        }

        public bool isConnect()
        {
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        public int ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                }
                catch (SqlException)
                {
                    return 0;
                }
            }
        }

        public int ExecuteQuery(string query, List<SqlParameter> Parametros)
        {
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        foreach (var p in Parametros)
                        {
                            command.Parameters.Add(p);
                        }
                        return command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    return 0;
                }
            }
        }

        public int ExecuteQuerySP(string query, List<SqlParameter> Parametros, out string msjError)
        {
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        msjError = string.Empty;
                        connection.Open();
                        foreach (var p in Parametros)
                        {
                            command.Parameters.Add(p);
                        }

                        command.CommandType = CommandType.StoredProcedure;
                        return command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    msjError = ex.Message;
                    return 0;
                }
            }
        }

        public DataSet ExecuteSelect(string query)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        var adapter = new SqlDataAdapter(query, connection);
                        //fill the dataset
                        adapter.Fill(ds);
                        return ds;
                    }
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
        }

        public DataSet ExecuteSelect(string query, List<SqlParameter> Parametros)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        foreach (var p in Parametros)
                        {
                            command.Parameters.Add(p);
                        }

                        var adapter = new SqlDataAdapter(query, connection);
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        return ds;
                    }
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
        }

        public DataTable ExecuteSelectDT(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        var adapter = new SqlDataAdapter(query, connection);
                        //fill the dataset
                        adapter.Fill(dt);
                        return dt;
                    }
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
        }

        public DataTable ExecuteSelectDT(string query, List<SqlParameter> Parametros)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        foreach (var p in Parametros)
                        {
                            command.Parameters.Add(p);
                        }

                        var adapter = new SqlDataAdapter(query, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand = command;
                        adapter.Fill(dt);
                        return dt;
                    }
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
        }

        public DataSet ExecuteStoreProcSelect(string query, List<SqlParameter> Parametros)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        foreach (var p in Parametros)
                        {
                            command.Parameters.Add(p);
                        }

                        var adapter = new SqlDataAdapter(query, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        return ds;
                    }
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
        }

        public DataSet ExecuteStoreProcSelect(string query)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        var adapter = new SqlDataAdapter(query, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        return ds;
                    }
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
        }


        #region "Async"

        public async Task<int> ExecuteQueryAsync(string query)
        {
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        return await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException)
                {
                    return 0;
                }
            }
        }

        public async Task<int> ExecuteQueryAsync(string query, List<SqlParameter> Parametros)
        {
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        foreach (var p in Parametros)
                        {
                            command.Parameters.Add(p);
                        }
                        return await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    return 0;
                }
            }
        }

        public async Task<int> ExecuteQuerySPAsync(string query, List<SqlParameter> Parametros)
        {
            using (SqlConnection connection = new SqlConnection(_strConexion))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        foreach (var p in Parametros)
                        {
                            command.Parameters.Add(p);
                        }

                        command.CommandType = CommandType.StoredProcedure;
                        return await command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    return 0;
                }
            }
        }

        #endregion

    }
}
