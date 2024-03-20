using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.Commons;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Login.Repository
{
    public class LoginRepository
    {
        private readonly SqlConnection _connection;
        private readonly string PROGRAM_DATA = "Select id, date, column_2, column_3, column_4, column_5 From Konkurencja.dbo.LaboProgramData " +
                                                "Where column_2 = 'dates' and column_3 = 'XXXX'";
        private readonly string UPDATE_TO_EXPIRE = "Update Konkurencja.dbo.LaboProgramData Set column_4 = 'Expire'";

        public LoginRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public ProgramDataDto GetProgramData(string password)
        {
            ProgramDataDto result = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                string query = PROGRAM_DATA.Replace("XXXX", password);
                cmd.CommandText = query;
                _connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (rdr.HasRows)
                {
                    result = new ProgramDataDto();
                    rdr.Read();
                    result.Id = rdr.GetByte(0);
                    result.Date = rdr.GetDateTime(1);
                    result.ColumnTwo = CommonFunction.DBNullToStringConv(rdr.GetValue(2));
                    result.ColumnThree = CommonFunction.DBNullToStringConv(rdr.GetValue(3));
                    result.ColumnFour = CommonFunction.DBNullToStringConv(rdr.GetValue(4));
                    result.ColumnFive = CommonFunction.DBNullToDoubleConv(rdr.GetValue(5));
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Load ProgramData.",
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu Load ProgramData.",
                    "Błąd połączenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            return result;
        }

        public void UpdateToExpire()
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = _connection;
                cmd.CommandText = UPDATE_TO_EXPIRE;
                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Set ProgramData to expire.",
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu Set ProgramData to expire.",
                    "Błąd połączenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

    }
}
