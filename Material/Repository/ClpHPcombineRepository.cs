using Laboratorium.ADO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using Laboratorium.Material.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Material.Repository
{
    internal class ClpHPcombineRepository : BasicCRUD<ClpHPcombineDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.MaterialClpHPcombineIndex;
        private static readonly string TABLE_NAME = Table.MATERIAL_HP_COMBINE_TABLE;

        public ClpHPcombineRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<ClpHPcombineDto> GetAll()
        {
            List<ClpHPcombineDto> list = new List<ClpHPcombineDto>();

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
                        string codeClass = CommonFunction.DBNullToStringConv(reader.GetValue(1));
                        string code = CommonFunction.DBNullToStringConv(reader.GetValue(2));
                        string description = CommonFunction.DBNullToStringConv(reader.GetValue(3));

                        ClpHPcombineDto materialH = new ClpHPcombineDto(materialId, codeClass, code, description);
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

        public override ClpHPcombineDto Save(ClpHPcombineDto data)
        {
            throw new System.NotImplementedException();
        }

        public override ClpHPcombineDto Update(ClpHPcombineDto data)
        {
            throw new System.NotImplementedException();
        }
    }
}
