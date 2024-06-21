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
    public class CmbClpSignalRepository : BasicCRUD<CmbClpSignalDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.CmbClpSignalIndex;
        private static readonly string TABLE_NAME = Table.CLP_SIGNAL_CODE_TABLE;

        public CmbClpSignalRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<CmbClpSignalDto> GetAll()
        {
            List<CmbClpSignalDto> list = new List<CmbClpSignalDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        byte id = reader.GetByte(0);
                        string namePl = CommonFunction.DBNullToStringConv(reader.GetValue(1));
                        string nameEn = CommonFunction.DBNullToStringConv(reader.GetValue(2));

                        CmbClpSignalDto signal = new CmbClpSignalDto(id, namePl, nameEn);
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

        public override CmbClpSignalDto Save(CmbClpSignalDto data)
        {
            throw new NotImplementedException();
        }

        public override CmbClpSignalDto Update(CmbClpSignalDto data)
        {
            throw new NotImplementedException();
        }
    }
}
