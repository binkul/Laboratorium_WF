using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.User.Repository
{
    public class UserRepository : ExtendedCRUD<UserDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.UserIndex;
        private static readonly string TABLE_NAME = Table.USER_TABLE;

        public UserRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<UserDto> GetAll()
        {
            List<UserDto> list = new List<UserDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        short id = reader.GetInt16(0);
                        string name = reader.GetString(1);
                        string surname = reader.GetString(2);
                        string email = reader.GetString(3);
                        string login = reader.GetString(4);
                        string permission = reader.GetString(5);
                        string identifier = reader.GetString(6);
                        bool active = reader.GetBoolean(7);
                        DateTime dateCreated = reader.GetDateTime(8);

                        UserDto user = new UserDto(id, name, surname, email, login, "", permission, identifier, active, dateCreated);
                        user.AcceptChanged();
                        list.Add(user);
                    }
                    reader.Close();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu GetAll " + _tableName,
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

            return list;
        }

        public UserDto GetUserByLoginAndPassword(string login, string password)
        {
            UserDto user = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                string query = SqlRead.ReadByName[_sqlIndex].Replace("XXXX", login);
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
                    user.DateCreated = rdr.GetDateTime(8);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Load User.",
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli '" + _tableName + "' w czasie operacji Load User: '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }

            return user;

        }

        public override UserDto Save(UserDto data)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = _connection;
                cmd.CommandText = SqlSave.Save[_sqlIndex];
                cmd.Parameters.AddWithValue("@name", data.Name);
                cmd.Parameters.AddWithValue("@surname", data.Surname);
                cmd.Parameters.AddWithValue("@e_mail", data.Email);
                cmd.Parameters.AddWithValue("@login", data.Login.ToLower());
                cmd.Parameters.AddWithValue("@password", data.Password);
                cmd.Parameters.AddWithValue("@permission", data.Permission);
                cmd.Parameters.AddWithValue("@identifier", data.Identifier);
                cmd.Parameters.AddWithValue("@active", data.Active);
                cmd.Parameters.AddWithValue("@date_created", data.DateCreated);

                _connection.Open();
                short id = Convert.ToInt16(cmd.ExecuteScalar());
                data.Id = id;
                data.GetCrudState = CrudState.OK;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Save " + _tableName,
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                data.GetCrudState = CrudState.ERROR;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli '" + _tableName + "' w czasie operacji Save: '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                data.GetCrudState = CrudState.ERROR;
            }
            finally
            {
                CloseConnection();
            }

            return data;
        }

        public override UserDto Update(UserDto data)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = _connection;
                cmd.CommandText = SqlUpdate.Update[_sqlIndex];
                cmd.Parameters.AddWithValue("@name", data.Name);
                cmd.Parameters.AddWithValue("@surname", data.Surname);
                cmd.Parameters.AddWithValue("@e_mail", data.Email);
                cmd.Parameters.AddWithValue("@login", data.Login.ToLower());
                cmd.Parameters.AddWithValue("@password", data.Password);
                cmd.Parameters.AddWithValue("@permission", data.Permission);
                cmd.Parameters.AddWithValue("@identifier", data.Identifier);
                cmd.Parameters.AddWithValue("@active", data.Active);
                cmd.Parameters.AddWithValue("@date_created", data.DateCreated);
                cmd.Parameters.AddWithValue("@id", data.Id);

                _connection.Open();
                cmd.ExecuteNonQuery();
                data.GetCrudState = CrudState.OK;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Update " + _tableName,
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                data.GetCrudState = CrudState.ERROR;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli '" + _tableName + "' w czasie operacji Update: '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                data.GetCrudState = CrudState.ERROR;
            }
            finally
            {
                CloseConnection();
            }

            return data;
        }

        public override void UpdateRow(DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
