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

namespace Laboratorium.LabBook.Repository
{
    public class LabBookNormTestRepository : BasicCRUD<LaboDataNormTestDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.LaboNormTestIndex;
        private static readonly string TABLE_NAME = Table.LABO_NORM_TEST_TABLE;
        private readonly IService _service;

        public LabBookNormTestRepository(SqlConnection connection, IService service) : base(connection, SQL_INDEX, TABLE_NAME)
        {
            _service = service;
        }

        public override IList<LaboDataNormTestDto> GetAll()
        {
            List<LaboDataNormTestDto> list = new List<LaboDataNormTestDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int days = reader.GetInt32(0);
                        int id = reader.GetInt32(1);
                        int laboId = reader.GetInt32(2);
                        short position = reader.GetInt16(3);
                        string norm = CommonFunction.DBNullToStringConv(reader.GetString(4));
                        string desc = CommonFunction.DBNullToStringConv(reader.GetString(5));
                        string requery = CommonFunction.DBNullToStringConv(reader.GetString(6));
                        string result = CommonFunction.DBNullToStringConv(reader.GetString(7));
                        string substrate = CommonFunction.DBNullToStringConv(reader.GetString(8));
                        string comment = CommonFunction.DBNullToStringConv(reader.GetString(9));
                        DateTime dateCreated = reader.GetDateTime(10);
                        DateTime dateUpdated = !reader.GetValue(11).Equals(DBNull.Value) ? reader.GetDateTime(11) : dateCreated;

                        LaboDataNormTestDto data = new LaboDataNormTestDto(days, id, laboId, position, norm, desc, requery, result, substrate, comment, dateCreated, dateUpdated, _service);

                        data.AcceptChanges();
                        list.Add(data);
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

        public override LaboDataNormTestDto Save(LaboDataNormTestDto data)
        {
            throw new NotImplementedException();
        }

        public override LaboDataNormTestDto Update(LaboDataNormTestDto data)
        {
            throw new NotImplementedException();
        }
    }
}
