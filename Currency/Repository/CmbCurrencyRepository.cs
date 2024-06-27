using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Currency.Repository
{
    public class CmbCurrencyRepository : BasicCRUD<CmbCurrencyDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.CmbCurrencyIndex;
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
                        string name = CommonFunction.DBNullToStringConv(reader.GetValue(1));
                        string curency = reader.GetString(2);
                        double rate = reader.GetDouble(3);

                        CmbCurrencyDto currency = new CmbCurrencyDto(id, name, curency, rate);
                        currency.AcceptChanges();
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
            CmbCurrencyDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@id", item.Id);
                command.Parameters.AddWithValue("@name", CommonFunction.NullStringToDBNullConv(item.Name));
                command.Parameters.AddWithValue("@currency", item.Currency);
                command.Parameters.AddWithValue("@rate", item.Rate);
                OpenConnection();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Save " + _tableName,
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

            return item;
        }

        public override CmbCurrencyDto Update(CmbCurrencyDto data)
        {
            CmbCurrencyDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlUpdate.Update[_sqlIndex];
                command.Parameters.AddWithValue("@name", CommonFunction.NullStringToDBNullConv(item.Name));
                command.Parameters.AddWithValue("@currency", item.Currency);
                command.Parameters.AddWithValue("@rate", item.Rate);
                command.Parameters.AddWithValue("@id", item.Id);
                OpenConnection();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Update " + _tableName,
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

            return item;
        }
    }
}
