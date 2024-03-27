using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Repository
{
    public class VocClassRepository : BasicCRUD<VocClassDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.VocClassIndex;
        private static readonly string TABLE_NAME = Table.VOC_CLASS_TABLE;

        public VocClassRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<VocClassDto> GetAll()
        {
            List<VocClassDto> list = new List<VocClassDto>();

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
                        string namePl = reader.GetString(1);

                        VocClassDto contrast = new VocClassDto(id, namePl);
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

        public override VocClassDto Save(VocClassDto data)
        {
            throw new NotImplementedException();
        }

        public override VocClassDto Update(VocClassDto data)
        {
            throw new NotImplementedException();
        }
    }
}
