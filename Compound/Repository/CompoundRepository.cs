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
    public class CompoundRepository : BasicCRUD<CompoundDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.CompoundIndex;
        private static readonly string TABLE_NAME = Table.COMPOUND_TABLE;

        public CompoundRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

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

                        CompoundDto composition = new CompoundDto(id, namePl, nameEn, shortPl, shortEn, index, cas, we, formula, isBio, dateCreated);
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
            throw new NotImplementedException();
        }

        public override CompoundDto Update(CompoundDto data)
        {
            throw new NotImplementedException();
        }
    }
}
