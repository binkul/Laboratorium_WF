using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Repository
{
    public class LabBookRepository : ExtendedCRUD<LaboDto>
    {
        public LabBookRepository(SqlConnection connection, SqlIndex sqlIndex, string tableName) : base(connection, sqlIndex, tableName)
        { }

        public override IList<LaboDto> GetAll()
        {
            List<LaboDto> list = new List<LaboDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string title = CommonFunction.DBNullToStringConv(reader.GetValue(1));
                        DateTime date = reader.GetDateTime(2);
                        long project = reader.GetInt64(3);
                        string target = CommonFunction.DBNullToStringConv(reader.GetValue(4));
                        string conclusion = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        double? density = CommonFunction.DBNullToDoubleConv(reader.GetValue(6));
                        string observation = CommonFunction.DBNullToStringConv(reader.GetValue(7));
                        bool deleted = reader.GetBoolean(8);

                        LaboDto labo = new LaboDto((int)id, title, date, (int)project, target, density, conclusion, observation, deleted);
                        labo.AcceptChanged();
                        list.Add(labo);
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

        public override LaboDto Save(LaboDto data)
        {
            throw new NotImplementedException();
        }

        public override LaboDto Update(LaboDto data)
        {
            throw new NotImplementedException();
        }

        public override void UpdateRow(DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
