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
    public class GlossClassRepository : BasicCRUD<CmbGlossClassDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.GlossClassIndex;
        private static readonly string TABLE_NAME = Table.GLOSS_CLASS_TABLE;

        public GlossClassRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<CmbGlossClassDto> GetAll()
        {
            List<CmbGlossClassDto> list = new List<CmbGlossClassDto>();

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
                        string nameEn = reader.GetString(2);

                        CmbGlossClassDto contrast = new CmbGlossClassDto(id, namePl, nameEn);
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

        public override CmbGlossClassDto Save(CmbGlossClassDto data)
        {
            throw new NotImplementedException();
        }

        public override CmbGlossClassDto Update(CmbGlossClassDto data)
        {
            throw new NotImplementedException();
        }
    }
}
