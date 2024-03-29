using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Repository
{
    public class LabBookRepository : ExtendedCRUD<LaboDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.LaboIndex;
        private static readonly string TABLE_NAME = Table.LABO_TABLE;
        private readonly IService _service;

        public LabBookRepository(SqlConnection connection, IService service) : base(connection, SQL_INDEX, TABLE_NAME)
        {
            _service = service;
        }

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
                        DateTime dateCreated = reader.GetDateTime(2);
                        DateTime dateUpdated = !reader.GetValue(3).Equals(DBNull.Value) ? reader.GetDateTime(3) : dateCreated;
                        long project = reader.GetInt32(4);
                        string target = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        string conclusion = CommonFunction.DBNullToStringConv(reader.GetValue(6));
                        double? density = CommonFunction.DBNullToDoubleConv(reader.GetValue(7));
                        string observation = CommonFunction.DBNullToStringConv(reader.GetValue(8));
                        bool deleted = reader.GetBoolean(9);
                        short userId = reader.GetInt16(10);

                        LaboDto labo = new LaboDto((int)id, title, dateCreated, dateUpdated, (int)project, target, density, conclusion, observation, deleted, userId, _service);
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
