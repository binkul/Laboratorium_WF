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
    public class MaterialHcodeRepository : BasicCRUD<MaterialClpHCodeDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.MaterialClpHcodeIndex;
        private static readonly string TABLE_NAME = Table.MATERIAL_H_CODE_TABLE;

        public MaterialHcodeRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<MaterialClpHCodeDto> GetAll()
        {
            List<MaterialClpHCodeDto> list = new List<MaterialClpHCodeDto>();

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
                        string classClp = CommonFunction.DBNullToStringConv(reader.GetValue(4));
                        string code = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        string description = CommonFunction.DBNullToStringConv(reader.GetValue(6));

                        MaterialClpHCodeDto materialH = new MaterialClpHCodeDto(materialId, codeId, classClp, code, description, comment, dateCreated);
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

        public override MaterialClpHCodeDto Save(MaterialClpHCodeDto data)
        {
            throw new NotImplementedException();
        }

        public override MaterialClpHCodeDto Update(MaterialClpHCodeDto data)
        {
            throw new NotImplementedException();
        }
    }
}
