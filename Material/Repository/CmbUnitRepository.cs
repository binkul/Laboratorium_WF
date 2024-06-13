using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.Material.Repository
{
    public class CmbUnitRepository : BasicCRUD<CmbUnitDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.UnitIndex;
        private static readonly string TABLE_NAME = Table.CMB_UNIT_TABLE;

        public CmbUnitRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<CmbUnitDto> GetAll()
        {
            List<CmbUnitDto> list = new List<CmbUnitDto>();

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
                        string description = CommonFunction.DBNullToStringConv(reader.GetValue(2));

                        CmbUnitDto unit = new CmbUnitDto(id, namePl, description);
                        list.Add(unit);
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

        public override CmbUnitDto Save(CmbUnitDto data)
        {
            throw new NotImplementedException();
        }

        public override CmbUnitDto Update(CmbUnitDto data)
        {
            throw new NotImplementedException();
        }
    }
}
