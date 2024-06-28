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
    public class MaterialCompoundRepository : BasicCRUD<MaterialCompoundDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.MaterialCompoundIndex;
        private static readonly string TABLE_NAME = Table.MATERIAL_COMPOUND_TABLE;

        public MaterialCompoundRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<MaterialCompoundDto> GetAll()
        {
            List<MaterialCompoundDto> list = new List<MaterialCompoundDto>();

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

                        MaterialCompoundDto composition = new MaterialCompoundDto(id, namePl, nameEn, shortPl, shortEn, index, cas, we, formula, isBio, dateCreated);
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

        public override MaterialCompoundDto Save(MaterialCompoundDto data)
        {
            throw new NotImplementedException();
        }

        public override MaterialCompoundDto Update(MaterialCompoundDto data)
        {
            throw new NotImplementedException();
        }
    }
}
