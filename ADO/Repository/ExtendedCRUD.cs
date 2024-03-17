using Laboratorium.ADO.SqlDataConstant;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.ADO.Repository
{
    public abstract class ExtendedCRUD<T> : BasicCRUD<T>, IExtendedCRUD<T>
    {
        public ExtendedCRUD(SqlConnection connection, SqlIndex sqlIndex, string tableName)
        : base(connection, sqlIndex, tableName) { }

        public abstract void UpdateRow(DataRow row);

        public virtual bool ExistById(long id)
        {
            bool result = false;

            try
            {
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlExist.ExistById[_sqlIndex] + id;
                OpenConnection();
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0) result = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu ExistById " + _tableName,
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli '" + _tableName + "': '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }

            return result;

        }

        public virtual bool ExistByName(string name)
        {
            bool result = false;

            try
            {
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlExist.ExistByName[_sqlIndex].Replace("XXXX", name);
                OpenConnection();
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0) result = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu ExistById " + _tableName,
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli '" + _tableName + "': '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        public virtual int CountByName(string name)
        {
            int count = 0;

            try
            {
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlCount.CountByName[_sqlIndex].Replace("XXXX", name);
                OpenConnection();
                count = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu CountByName " + _tableName,
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie odczytu danych z tabeli '" + _tableName + "': '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }

            return count;
        }
    }
}
