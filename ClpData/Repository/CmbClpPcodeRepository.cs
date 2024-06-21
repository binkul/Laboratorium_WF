using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.ClpData.Repository
{
    public class CmbClpPcodeRepository : BasicCRUD<CmbClpPcodeDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.CmbClpPcodeIndex;
        private static readonly string TABLE_NAME = Table.CLP_P_CODE_TABLE;

        public CmbClpPcodeRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<CmbClpPcodeDto> GetAll()
        {
            List<CmbClpPcodeDto> list = new List<CmbClpPcodeDto>();

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
                        string code = reader.GetString(2);
                        string description = reader.GetString(3);
                        int ordering = reader.GetInt32(4);
                        DateTime date = reader.GetDateTime(5);

                        CmbClpPcodeDto contrast = new CmbClpPcodeDto(id, code, description, ordering, date);
                        list.Add(contrast);
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

        public override CmbClpPcodeDto Save(CmbClpPcodeDto data)
        {
            throw new NotImplementedException();
        }

        public override CmbClpPcodeDto Update(CmbClpPcodeDto data)
        {
            throw new NotImplementedException();
        }
    }
}
