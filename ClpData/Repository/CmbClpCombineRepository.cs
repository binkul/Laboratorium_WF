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

namespace Laboratorium.ClpData.Repository
{
    public class CmbClpCombineRepository : BasicCRUD<CmbClpCombineDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.CmbClpCombineCodeIndex;
        private static readonly string TABLE_NAME = Table.CLP_HP_CODE_TABLE;

        public CmbClpCombineRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<CmbClpCombineDto> GetAll()
        {
            List<CmbClpCombineDto> list = new List<CmbClpCombineDto>();

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
                        string className = CommonFunction.DBNullToStringConv(reader.GetValue(1));
                        string code = CommonFunction.DBNullToStringConv(reader.GetValue(2));
                        string desc = CommonFunction.DBNullToStringConv(reader.GetValue(3));
                        int ord = reader.GetInt32(4);
                        string signal = CommonFunction.DBNullToStringConv(reader.GetValue(5));

                        CmbClpCombineDto combineCode = new CmbClpCombineDto(id, className, code, desc, ord, signal);
                        list.Add(combineCode);
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

        public override CmbClpCombineDto Save(CmbClpCombineDto data)
        {
            throw new NotImplementedException();
        }

        public override CmbClpCombineDto Update(CmbClpCombineDto data)
        {
            throw new NotImplementedException();
        }
    }
}
