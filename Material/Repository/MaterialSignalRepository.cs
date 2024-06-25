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

namespace Laboratorium.Material.Repository
{
    public class MaterialSignalRepository : BasicCRUD<MaterialClpSignalDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.MaterialClpSignalIndex;
        private static readonly string TABLE_NAME = Table.MATERIAL_SIGNAL_CODE_TABLE;

        public MaterialSignalRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<MaterialClpSignalDto> GetAll()
        {
            List<MaterialClpSignalDto> list = new List<MaterialClpSignalDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int materialId = reader.GetInt32(0);
                        byte codeId = reader.GetByte(1);
                        DateTime dateCreated = reader.GetDateTime(2);
                        string namePl = CommonFunction.DBNullToStringConv(reader.GetValue(3));

                        MaterialClpSignalDto signal = new MaterialClpSignalDto(materialId, codeId, namePl, dateCreated);
                        list.Add(signal);
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

        public override IList<MaterialClpSignalDto> GetAllByLaboId(int materialId)
        {
            List<MaterialClpSignalDto> list = new List<MaterialClpSignalDto>();

            try
            {
                string query = SqlRead.ReadByName[_sqlIndex] + materialId.ToString();
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int matId = reader.GetInt32(0);
                        byte codeId = reader.GetByte(1);
                        DateTime dateCreated = reader.GetDateTime(2);
                        string namePl = CommonFunction.DBNullToStringConv(reader.GetValue(3));

                        MaterialClpSignalDto signal = new MaterialClpSignalDto(materialId, codeId, namePl, dateCreated);
                        list.Add(signal);
                    }
                    reader.Close();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu GetAllByMaterialId " + _tableName,
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

        public override MaterialClpSignalDto Save(MaterialClpSignalDto data)
        {
            MaterialClpSignalDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@material_id", item.MaterialId);
                command.Parameters.AddWithValue("@code_id", item.CodeId);
                command.Parameters.AddWithValue("@date_created", item.DateCreated);
                OpenConnection();
                command.ExecuteNonQuery();
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

        public override MaterialClpSignalDto Update(MaterialClpSignalDto data)
        {
            throw new NotImplementedException();
        }
    }
}
