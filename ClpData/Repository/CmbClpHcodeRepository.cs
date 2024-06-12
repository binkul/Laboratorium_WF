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
    public class CmbClpHcodeRepository : BasicCRUD<CmbClpHcodeDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.ClpHcodeIndex;
        private static readonly string TABLE_NAME = Table.CLP_H_CODE_TABLE;

        public CmbClpHcodeRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<CmbClpHcodeDto> GetAll()
        {
            List<CmbClpHcodeDto> list = new List<CmbClpHcodeDto>();

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
                        string classClp = reader.GetString(1);
                        string code = reader.GetString(2);
                        string description = reader.GetString(3);
                        int ordering = reader.GetInt32(4);
                        byte ghsId = reader.GetByte(5);
                        string ghsDesc = reader.GetString(6);
                        byte sigId = reader.GetByte(7);
                        string sigDesc = reader.GetString(8);
                        DateTime date = reader.GetDateTime(9);

                        CmbClpHcodeDto contrast = new CmbClpHcodeDto(id, classClp, code, description, ordering, ghsId, ghsDesc, sigId, sigDesc, date);
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

        public override CmbClpHcodeDto Save(CmbClpHcodeDto data)
        {
            throw new NotImplementedException();
        }

        public override CmbClpHcodeDto Update(CmbClpHcodeDto data)
        {
            throw new NotImplementedException();
        }
    }
}
