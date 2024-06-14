using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Currency.Repository
{
    public class CmbCurrencyRepository : BasicCRUD<CmbCurrencyDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.CurrencyIndex;
        private static readonly string TABLE_NAME = Table.CMB_CURRENCY_TABLE;

        public CmbCurrencyRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<CmbCurrencyDto> GetAll()
        {
            List<CmbCurrencyDto> list = new List<CmbCurrencyDto>();

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
                        string name = reader.GetString(1);
                        double rate = reader.GetDouble(2);

                        CmbCurrencyDto currency = new CmbCurrencyDto(id, name, rate);
                        list.Add(currency);
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

        public override CmbCurrencyDto Save(CmbCurrencyDto data)
        {
            throw new NotImplementedException();
        }

        public override CmbCurrencyDto Update(CmbCurrencyDto data)
        {
            throw new NotImplementedException();
        }
    }
}
