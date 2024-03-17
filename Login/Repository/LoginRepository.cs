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
        private readonly string GET_USER_BY_LOGIN_AND_PASSWORD = "Select id, name, surname, e_mail, login, permission, " +
                                                            "identifier, active, date_created from Konkurencja.dbo.LaboUsers Where login = 'XXXX' and password = 'YYYY'";
        private readonly string SAVE_USER = "Insert Into Konkurencja.dbo.LaboUsers(name, surname, e_mail, login, password, permission, identifier, active, date_created) " +
                                            "Values(@name, @surname, @e_mail, @login, @password, @permission, @identifier, @active, @date_created);";
        private readonly string IS_USER_EXIST = "Select count(*) as exist From Konkurencja.dbo.LaboUsers Where login = 'XXXX'";

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

        public UserDto GetUserByLoginAndPassword(string login, string password)
        {
            UserDto user = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                string query = GET_USER_BY_LOGIN_AND_PASSWORD.Replace("XXXX", login);
                query = query.Replace("YYYY", password);
                cmd.CommandText = query;
                _connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (rdr.HasRows)
                {
                    user = new UserDto();
                    rdr.Read();
                    user.Id = rdr.GetInt16(0);
                    user.Name = CommonFunction.DBNullToStringConv(rdr.GetValue(1));
                    user.Surname = CommonFunction.DBNullToStringConv(rdr.GetValue(2));
                    user.Email = CommonFunction.DBNullToStringConv(rdr.GetValue(3));
                    user.Login = rdr.GetString(4);
                    user.Permission = rdr.GetString(5);
                    user.Identifier = rdr.GetString(6);
                    user.Active = rdr.GetBoolean(7);
                    user.Date = rdr.GetDateTime(8);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Load User.",
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu Load User.",
                    "Błąd połączenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            return user;
        }

        public void SaveUser(UserDto user)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = _connection;
                cmd.CommandText = SAVE_USER;
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@surname", user.Surname);
                cmd.Parameters.AddWithValue("@e_mail", user.Email);
                cmd.Parameters.AddWithValue("@login", user.Login.ToLower());
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@permission", user.Permission);
                cmd.Parameters.AddWithValue("@identifier", user.Identifier);
                cmd.Parameters.AddWithValue("@active", user.Active);
                cmd.Parameters.AddWithValue("@date_created", user.Date);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Save Users.",
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu Save Users.",
                    "Błąd połączenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }

        }

        public bool UserExist(string login)
        {
            bool result = false;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                string query = IS_USER_EXIST.Replace("XXXX", login);
                cmd.CommandText = query;
                _connection.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Is Users exist.",
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu Is Users exist.",
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
