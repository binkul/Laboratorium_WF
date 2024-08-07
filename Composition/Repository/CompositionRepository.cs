using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Composition.Repository
{
    public class CompositionRepository : BasicCRUD<CompositionHistoryDto>
    {
        private const string GET_LAST_BY_ID = "Select TOP 1 id, labo_id, [version], mass, change_type, comments, login_id, date_created " +
            "From Konkurencja.dbo.LaboCompositionHistory Where labo_id=XXXX And[version] = " +
            "(Select MAX([version]) From Konkurencja.dbo.LaboCompositionHistory Where labo_id=XXXX) Order by id Desc";

        private static readonly SqlIndex SQL_INDEX = SqlIndex.CompositionHistory;
        private static readonly string TABLE_NAME = Table.COMPOSITION_HISTORY_TABLE;

        public CompositionRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public CompositionHistoryDto GetLastFromLaboId(int laboId, short loginId)
        {
            CompositionHistoryDto composition = new CompositionHistoryDto(laboId, loginId);

            try
            {
                string query = GET_LAST_BY_ID.Replace("XXXX", laboId.ToString());
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    int id = reader.GetInt32(0);
                    int labo = reader.GetInt32(1);
                    int version = reader.GetInt32(2);
                    double mass = reader.GetDouble(3);
                    string changeType = CommonFunction.DBNullToStringConv(reader.GetValue(4));
                    string comments = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                    short loginId = reader.GetInt16(6);
                    DateTime dateCreated = reader.GetDateTime(7);
                    composition = new CompositionHistoryDto(id, laboId, version, mass, changeType, comments, loginId, dateCreated);
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

            return composition;
        }

        public override IList<CompositionHistoryDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public override CompositionHistoryDto Save(CompositionHistoryDto data)
        {
            throw new NotImplementedException();
        }

        public override CompositionHistoryDto Update(CompositionHistoryDto data)
        {
            throw new NotImplementedException();
        }
    }
}
