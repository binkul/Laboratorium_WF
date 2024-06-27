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
    public class CmbMaterialFunctionRepository : BasicCRUD<CmbMaterialFunctionDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.CmbMaterialFunctionIndex;
        private static readonly string TABLE_NAME = Table.CMB_MAT_FUNCTION_TABLE;

        public CmbMaterialFunctionRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<CmbMaterialFunctionDto> GetAll()
        {
            List<CmbMaterialFunctionDto> list = new List<CmbMaterialFunctionDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        short id = reader.GetInt16(0);
                        string namePl = reader.GetString(1);

                        CmbMaterialFunctionDto function = new CmbMaterialFunctionDto(id, namePl);
                        function.AcceptChanges();
                        list.Add(function);
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

        public override CmbMaterialFunctionDto Save(CmbMaterialFunctionDto data)
        {
            CmbMaterialFunctionDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@id", item.Id);
                command.Parameters.AddWithValue("@name_pl", item.NamePl);
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

        public override CmbMaterialFunctionDto Update(CmbMaterialFunctionDto data)
        {
            CmbMaterialFunctionDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlUpdate.Update[_sqlIndex];
                command.Parameters.AddWithValue("@id", item.Id);
                command.Parameters.AddWithValue("@name_pl", item.NamePl);
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
