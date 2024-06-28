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
    public class MaterialCompositionRepository : BasicCRUD<MaterialCompositionDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.MaterialCompositionIndex;
        private static readonly string TABLE_NAME = Table.MATERIAL_COMPOSITION_TABLE;

        public MaterialCompositionRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<MaterialCompositionDto> GetAll()
        {
            List<MaterialCompositionDto> list = new List<MaterialCompositionDto>();

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
                        int materialId = reader.GetInt32(1);
                        double min = reader.GetDouble(2);
                        double max = reader.GetDouble(3);
                        byte ord = reader.GetByte(4);
                        string remarks = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        DateTime dateCreated = reader.GetDateTime(3);

                        MaterialCompositionDto composition = new MaterialCompositionDto(id, materialId, min, max, ord, remarks, dateCreated);
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

        public override IList<MaterialCompositionDto> GetAllByLaboId(int materialId)
        {
            List<MaterialCompositionDto> list = new List<MaterialCompositionDto>();

            try
            {
                string query = SqlRead.ReadByName[_sqlIndex];
                query = query.Replace("XXXX", materialId.ToString());
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        double min = reader.GetDouble(1);
                        double max = reader.GetDouble(2);
                        byte ord = reader.GetByte(3);
                        string remarks = CommonFunction.DBNullToStringConv(reader.GetValue(4));
                        DateTime dateCreated = reader.GetDateTime(5);

                        MaterialCompositionDto composition = new MaterialCompositionDto(id, materialId, min, max, ord, remarks, dateCreated);
                        composition.AcceptChanges();
                        list.Add(composition);
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

        public override MaterialCompositionDto Save(MaterialCompositionDto data)
        {
            throw new NotImplementedException();
        }

        public override MaterialCompositionDto Update(MaterialCompositionDto data)
        {
            throw new NotImplementedException();
        }
    }
}
