using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Material.Repository
{
    public class MaterialGHSRepository : BasicCRUD<MaterialClpGhsDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.MaterialClpGhsIndex;
        private static readonly string TABLE_NAME = Table.MATERIAL_GHS_CODE_TABLE;

        public MaterialGHSRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<MaterialClpGhsDto> GetAll()
        {
            List<MaterialClpGhsDto> list = new List<MaterialClpGhsDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int materialId= reader.GetInt32(0);
                        byte codeId = reader.GetByte(1);
                        DateTime dateCreated = reader.GetDateTime(2);

                        MaterialClpGhsDto ghs = new MaterialClpGhsDto(materialId, codeId, dateCreated);
                        list.Add(ghs);
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

        public override IList<MaterialClpGhsDto> GetAllByLaboId(int materialId)
        {
            List<MaterialClpGhsDto> list = new List<MaterialClpGhsDto>();

            try
            {
                string query = SqlRead.ReadByName[_sqlIndex].Replace("XXXX", materialId.ToString());
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

                        MaterialClpGhsDto ghs = new MaterialClpGhsDto(matId, codeId, dateCreated);
                        list.Add(ghs);
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

        public override MaterialClpGhsDto Save(MaterialClpGhsDto data)
        {
            throw new NotImplementedException();
        }

        public override MaterialClpGhsDto Update(MaterialClpGhsDto data)
        {
            throw new NotImplementedException();
        }
    }
}
