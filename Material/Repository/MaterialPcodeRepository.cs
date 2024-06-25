using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Material.Repository
{
    public class MaterialPcodeRepository : BasicCRUD<MaterialClpPCodeDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.MaterialClpPcodeIndex;
        private static readonly string TABLE_NAME = Table.MATERIAL_P_CODE_TABLE;

        public MaterialPcodeRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<MaterialClpPCodeDto> GetAll()
        {
            List<MaterialClpPCodeDto> list = new List<MaterialClpPCodeDto>();

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
                        short codeId = reader.GetInt16(1);
                        string comment = CommonFunction.DBNullToStringConv(reader.GetValue(2));
                        DateTime dateCreated = reader.GetDateTime(3);
                        string code = CommonFunction.DBNullToStringConv(reader.GetValue(4));
                        string description = CommonFunction.DBNullToStringConv(reader.GetValue(5));

                        MaterialClpPCodeDto materialH = new MaterialClpPCodeDto(materialId, codeId, code, description, comment, dateCreated);
                        list.Add(materialH);
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

        public override MaterialClpPCodeDto Save(MaterialClpPCodeDto data)
        {
            MaterialClpPCodeDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@material_id", item.MaterialId);
                command.Parameters.AddWithValue("@code_id", item.CodeId);
                command.Parameters.AddWithValue("@comments", CommonFunction.NullStringToDBNullConv(item.Comment));
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

        public override MaterialClpPCodeDto Update(MaterialClpPCodeDto data)
        {
            throw new NotImplementedException();
        }
    }
}
