using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Material.Repository
{
    public class CompoundRepository : BasicCRUD<CompoundDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.CompoundIndex;
        private static readonly string TABLE_NAME = Table.COMPOUND_TABLE;
        private readonly IService _service;
        public CompoundRepository(SqlConnection connection, IService service) : base(connection, SQL_INDEX, TABLE_NAME)
        {
            _service = service;
        }

        public override IList<CompoundDto> GetAll()
        {
            List<CompoundDto> list = new List<CompoundDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string namePl = reader.GetString(1);
                        string nameEn = CommonFunction.DBNullToStringConv(reader.GetValue(2));
                        string shortPl = reader.GetString(3);
                        string shortEn = CommonFunction.DBNullToStringConv(reader.GetValue(4));
                        string index = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        string cas = CommonFunction.DBNullToStringConv(reader.GetValue(6));
                        string we = CommonFunction.DBNullToStringConv(reader.GetValue(7));
                        string formula = CommonFunction.DBNullToStringConv(reader.GetValue(8));
                        bool isBio = reader.GetBoolean(9);
                        DateTime dateCreated = reader.GetDateTime(10);

                        CompoundDto composition = new CompoundDto(id, namePl, nameEn, shortPl, shortEn, index, cas, we, formula, isBio, dateCreated, _service);
                        composition.AcceptChanges();
                        list.Add(composition);
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

        public override CompoundDto Save(CompoundDto data)
        {
            CompoundDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@name_pl", item.NamePl);
                command.Parameters.AddWithValue("@name_en", CommonFunction.NullStringToDBNullConv(item.NameEn));
                command.Parameters.AddWithValue("@short_pl", item.ShortPl);
                command.Parameters.AddWithValue("@short_en", CommonFunction.NullStringToDBNullConv(item.ShortEn));
                command.Parameters.AddWithValue("@index_nr", CommonFunction.NullStringToDBNullConv(item.Index));
                command.Parameters.AddWithValue("@cas", CommonFunction.NullStringToDBNullConv(item.CAS));
                command.Parameters.AddWithValue("@we", CommonFunction.NullStringToDBNullConv(item.WE));
                command.Parameters.AddWithValue("@formula", CommonFunction.NullStringToDBNullConv(item.Formula));
                command.Parameters.AddWithValue("@is_bio", item.IsBio);
                command.Parameters.AddWithValue("@date_created", item.DateCreated);
                OpenConnection();
                int id = Convert.ToInt16(command.ExecuteScalar());
                item.Id = id;
                item.CrudState = CrudState.OK;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Save " + _tableName,
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                item.CrudState = CrudState.ERROR;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli '" + _tableName + "': '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                item.CrudState = CrudState.ERROR;
            }
            finally
            {
                CloseConnection();
            }

            return item;
        }

        public override CompoundDto Update(CompoundDto data)
        {
            CompoundDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlUpdate.Update[_sqlIndex];
                command.Parameters.AddWithValue("@name_pl", item.NamePl);
                command.Parameters.AddWithValue("@name_en", CommonFunction.NullStringToDBNullConv(item.NameEn));
                command.Parameters.AddWithValue("@short_pl", item.ShortPl);
                command.Parameters.AddWithValue("@short_en", CommonFunction.NullStringToDBNullConv(item.ShortEn));
                command.Parameters.AddWithValue("@index_nr", CommonFunction.NullStringToDBNullConv(item.Index));
                command.Parameters.AddWithValue("@cas", CommonFunction.NullStringToDBNullConv(item.CAS));
                command.Parameters.AddWithValue("@we", CommonFunction.NullStringToDBNullConv(item.WE));
                command.Parameters.AddWithValue("@formula", CommonFunction.NullStringToDBNullConv(item.Formula));
                command.Parameters.AddWithValue("@is_bio", item.IsBio);
                command.Parameters.AddWithValue("@id", item.Id);
                OpenConnection();
                command.ExecuteNonQuery();
                item.CrudState = CrudState.OK;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Update " + _tableName,
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                item.CrudState = CrudState.ERROR;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli '" + _tableName + "': '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                item.CrudState = CrudState.ERROR;
            }
            finally
            {
                CloseConnection();
            }

            return item;
        }

        public new bool DeleteById(long id)
        {
            SqlCommand command = new SqlCommand();
            bool result = true;

            try
            {
                command.Connection = _connection;
                command.CommandText = "Delete From Konkurencja.dbo.MaterialComposition Where compound_id=" + id.ToString();
                OpenConnection();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Delete " + _tableName,
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
    }
}
