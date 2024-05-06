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
    public class LabBookContrastRepository : BasicCRUD<LaboDataContrastDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.LaboContrastIndex;
        private static readonly string TABLE_NAME = Table.LABO_CONTRAST_TABLE;
        private readonly IService _service;

        public LabBookContrastRepository(SqlConnection connection, IService service) : base(connection, SQL_INDEX, TABLE_NAME)
        {
            _service = service;
        }

        public override IList<LaboDataContrastDto> GetAll()
        {
            List<LaboDataContrastDto> list = new List<LaboDataContrastDto>();

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
                        int laboId = reader.GetInt32(1);
                        bool isDeleted = reader.GetBoolean(2);
                        string applicator = CommonFunction.DBNullToStringConv(reader.GetValue(3));
                        short position = reader.GetInt16(4);
                        string substrate = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        double? contrast = CommonFunction.DBNullToDoubleConv(reader.GetValue(6));
                        double? tw = CommonFunction.DBNullToDoubleConv(reader.GetValue(7));
                        double? sp = CommonFunction.DBNullToDoubleConv(reader.GetValue(8));
                        string comments = CommonFunction.DBNullToStringConv(reader.GetValue(9));
                        DateTime dateCreated = reader.GetDateTime(10);
                        DateTime dateUpdated = !reader.GetValue(11).Equals(DBNull.Value) ? reader.GetDateTime(11) : dateCreated;

                        LaboDataContrastDto data = new LaboDataContrastDto(id, laboId, dateCreated, isDeleted, applicator, position, substrate, contrast, tw, sp, comments, dateUpdated, _service);

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

        public override LaboDataContrastDto Save(LaboDataContrastDto data)
        {
            LaboDataContrastDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@labo_id", item.LaboId);
                command.Parameters.AddWithValue("@is_deleted", item.IsDeleted);
                command.Parameters.AddWithValue("@applicator_name", CommonFunction.NullStringToDBNullConv(item.Applicator));
                command.Parameters.AddWithValue("@position", item.Position);
                command.Parameters.AddWithValue("@substrate", CommonFunction.NullStringToDBNullConv(item.Substrate));
                command.Parameters.AddWithValue("@contrast", CommonFunction.NullDoubleToDBNullConv(item.Contrast));
                command.Parameters.AddWithValue("@tw", CommonFunction.NullDoubleToDBNullConv(item.Tw));
                command.Parameters.AddWithValue("@sp", CommonFunction.NullDoubleToDBNullConv(item.Sp));
                command.Parameters.AddWithValue("@comments", CommonFunction.NullStringToDBNullConv(item.Comments));
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

        public override LaboDataContrastDto Update(LaboDataContrastDto data)
        {
            LaboDataContrastDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlUpdate.Update[_sqlIndex];
                command.Parameters.AddWithValue("@is_deleted", item.IsDeleted);
                command.Parameters.AddWithValue("@applicator_name", CommonFunction.NullStringToDBNullConv(item.Applicator));
                command.Parameters.AddWithValue("@position", item.Position);
                command.Parameters.AddWithValue("@substrate", CommonFunction.NullStringToDBNullConv(item.Substrate));
                command.Parameters.AddWithValue("@contrast", CommonFunction.NullDoubleToDBNullConv(item.Contrast));
                command.Parameters.AddWithValue("@tw", CommonFunction.NullDoubleToDBNullConv(item.Tw));
                command.Parameters.AddWithValue("@sp", CommonFunction.NullDoubleToDBNullConv(item.Sp));
                command.Parameters.AddWithValue("@comments", CommonFunction.NullStringToDBNullConv(item.Comments));
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
