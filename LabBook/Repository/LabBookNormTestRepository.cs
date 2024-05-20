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

namespace Laboratorium.LabBook.Repository
{
    public class LabBookNormTestRepository : BasicCRUD<LaboDataNormTestDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.LaboNormTestIndex;
        private static readonly string TABLE_NAME = Table.LABO_NORM_TEST_TABLE;
        private readonly IDgvService _service;

        public LabBookNormTestRepository(SqlConnection connection, IDgvService service) : base(connection, SQL_INDEX, TABLE_NAME)
        {
            _service = service;
        }

        public override IList<LaboDataNormTestDto> GetAll()
        {
            List<LaboDataNormTestDto> list = new List<LaboDataNormTestDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int days = reader.GetInt32(0);
                        int id = reader.GetInt32(1);
                        int laboId = reader.GetInt32(2);
                        short position = reader.GetInt16(3);
                        string norm = CommonFunction.DBNullToStringConv(reader.GetValue(4));
                        string desc = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        string requery = CommonFunction.DBNullToStringConv(reader.GetValue(6));
                        string result = CommonFunction.DBNullToStringConv(reader.GetValue(7));
                        string substrate = CommonFunction.DBNullToStringConv(reader.GetValue(8));
                        string comment = CommonFunction.DBNullToStringConv(reader.GetValue(9));
                        DateTime dateCreated = reader.GetDateTime(10);
                        DateTime dateUpdated = !reader.GetValue(11).Equals(DBNull.Value) ? reader.GetDateTime(11) : dateCreated;

                        LaboDataNormTestDto data = new LaboDataNormTestDto(days, id, laboId, position, norm, desc, requery, result, substrate, comment, dateCreated, dateUpdated, _service);

                        data.AcceptChanges();
                        list.Add(data);
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

        public override LaboDataNormTestDto Save(LaboDataNormTestDto data)
        {
            LaboDataNormTestDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@labo_id", item.LaboId);
                command.Parameters.AddWithValue("@position", item.Position);
                command.Parameters.AddWithValue("@norm", CommonFunction.NullStringToDBNullConv(item.Norm));
                command.Parameters.AddWithValue("@description", CommonFunction.NullStringToDBNullConv(item.Description));
                command.Parameters.AddWithValue("@requirement", CommonFunction.NullStringToDBNullConv(item.Requirement));
                command.Parameters.AddWithValue("@result", CommonFunction.NullStringToDBNullConv(item.Result));
                command.Parameters.AddWithValue("@substarte", CommonFunction.NullStringToDBNullConv(item.Substrate));
                command.Parameters.AddWithValue("@comment", CommonFunction.NullStringToDBNullConv(item.Comments));
                command.Parameters.AddWithValue("@date_created", item.DateCreated);
                command.Parameters.AddWithValue("@date_updated", item.DateUpdated);
                OpenConnection();
                int id = Convert.ToInt32(command.ExecuteScalar());
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

        public override LaboDataNormTestDto Update(LaboDataNormTestDto data)
        {
            LaboDataNormTestDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlUpdate.Update[_sqlIndex];
                command.Parameters.AddWithValue("@position", item.Position);
                command.Parameters.AddWithValue("@norm", CommonFunction.NullStringToDBNullConv(item.Norm));
                command.Parameters.AddWithValue("@description", CommonFunction.NullStringToDBNullConv(item.Description));
                command.Parameters.AddWithValue("@requirement", CommonFunction.NullStringToDBNullConv(item.Requirement));
                command.Parameters.AddWithValue("@result", CommonFunction.NullStringToDBNullConv(item.Result));
                command.Parameters.AddWithValue("@substarte", CommonFunction.NullStringToDBNullConv(item.Substrate));
                command.Parameters.AddWithValue("@comment", CommonFunction.NullStringToDBNullConv(item.Comments));
                command.Parameters.AddWithValue("@date_created", item.DateCreated);
                command.Parameters.AddWithValue("@date_updated", item.DateUpdated);
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
    }
}
