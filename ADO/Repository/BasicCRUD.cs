using Laboratorium.ADO.SqlDataConstant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.ADO.Repository
{
    public abstract class BasicCRUD<T> : IBasicCRUD<T>
    {
        protected SqlConnection _connection;
        protected string _tableName;
        protected SqlIndex _sqlIndex;

        public BasicCRUD(SqlConnection connection, SqlIndex sqlIndex, string tableName)
        {
            _connection = connection;
            _tableName = tableName;
            _sqlIndex = sqlIndex;
        }

        public virtual DataTable GetAllAsTable(bool createKey = true)
        {
            DataTable dataTable = new DataTable();

            try
            {
                string SqlQuery = SqlRead.Read[_sqlIndex];
                SqlDataAdapter dataAdapter = new SqlDataAdapter(SqlQuery, _connection);
                dataAdapter.Fill(dataTable);
                if (createKey)
                {
                    SetPrimaryColumn(dataTable);
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show("Błąd SQL w czasie odczytu danych z tabeli '" + _tableName + "': '" + e.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Błąd systemowy w czasie odczytu danych z tabeli '" + _tableName + "': '" + e.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }

            return dataTable;

        }

        public abstract IList<T> GetAll();

        public virtual IList<T> GetAllByLaboId(int laboId)
        {
            return null;
        }

        public virtual bool DeleteById(long id)
        {
            SqlCommand command = new SqlCommand();
            bool result = true;

            try
            {
                command.Connection = _connection;
                command.CommandText = SqlDelete.Delete[_sqlIndex] + id.ToString();
                OpenConnection();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Read " + _tableName,
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli '" + _tableName + "': '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        public abstract T Save(T data);

        public abstract T Update(T data);

        private static DataTable SetPrimaryColumn(DataTable table)
        {
            DataTable dataTable = table;
            if (dataTable.Columns.Contains("id"))
            {
                DataColumn[] key = new DataColumn[1];
                key[0] = dataTable.Columns["id"];
                dataTable.PrimaryKey = key;
            }

            return dataTable;
        }

        protected void CloseConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        protected void OpenConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Open)
                _connection.Open();
        }

    }
}
