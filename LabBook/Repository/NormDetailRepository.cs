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

namespace Laboratorium.LabBook.Repository
{
    public class NormDetailRepository : BasicCRUD<NormDetailDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.NormDetailIndex;
        private static readonly string TABLE_NAME = Table.NORM_DETAIL_TABLE;

        public NormDetailRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<NormDetailDto> GetAll()
        {
            List<NormDetailDto> list = new List<NormDetailDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        short id = reader.GetInt16(0);
                        short normId = reader.GetInt16(1);
                        string substrate = CommonFunction.DBNullToStringConv(reader.GetValue(2));
                        string detail = CommonFunction.DBNullToStringConv(reader.GetValue(3));

                        NormDetailDto norm = new NormDetailDto(id, normId, substrate, detail);
                        list.Add(norm);
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

        public override NormDetailDto Save(NormDetailDto data)
        {
            throw new System.NotImplementedException();
        }

        public override NormDetailDto Update(NormDetailDto data)
        {
            throw new System.NotImplementedException();
        }
    }
}
