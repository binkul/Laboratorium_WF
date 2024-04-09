using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using Laboratorium.LabBook.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Repository
{
    public class LabBookViscosityColRepository : BasicCRUD<LaboDataViscosityColDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.LaboViscosityColIndex;
        private static readonly string TABLE_NAME = Table.LABO_VISC_COL_DATA_TABLE;

        public LabBookViscosityColRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<LaboDataViscosityColDto> GetAllByLaboId(int laboId)
        {
            IList<LaboDataViscosityColDto> list = null;

            try
            {
                string sql = SqlRead.ReadByName[_sqlIndex] + laboId.ToString();
                SqlCommand command = new SqlCommand(sql, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string type = reader.GetString(1);
                        string columns = CommonFunction.DBNullToStringConv(reader.GetValue(2));

                        if (!string.IsNullOrEmpty(type) && Enum.TryParse(type, out Profile profile))
                        {
                            LaboDataViscosityColDto col = new LaboDataViscosityColDto(id, profile, columns);
                            list = new List<LaboDataViscosityColDto>();
                            list.Add(col);
                        }
                    }
                    reader.Close();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu GetById " + _tableName,
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

        public override IList<LaboDataViscosityColDto> GetAll()
        {
            List<LaboDataViscosityColDto> list = new List<LaboDataViscosityColDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int laboId = reader.GetInt32(0);
                        string type = reader.GetString(1);
                        string columns = CommonFunction.DBNullToStringConv(reader.GetValue(2));

                        LaboDataViscosityColDto visProfile;
                        if (Enum.TryParse(type, out Profile profile))
                        {
                            visProfile = new LaboDataViscosityColDto(laboId, profile, columns);
                        }
                        else
                        {
                            visProfile = new LaboDataViscosityColDto(laboId, Profile.STD_X, columns);
                        }

                        list.Add(visProfile);
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

        public override LaboDataViscosityColDto Save(LaboDataViscosityColDto data)
        {
            LaboDataViscosityColDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@labo_id", item.LaboId);
                command.Parameters.AddWithValue("@type", item.Profile.ToString());
                command.Parameters.AddWithValue("@columns", CommonFunction.NullStringToDBNullConv(item.Columns));
                OpenConnection();
                command.ExecuteNonQuery();
                item.CrudState = CrudState.OK;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Save " + _tableName,
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                item.CrudState = CrudState.ERROR;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli '" + _tableName + "': '" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                item.CrudState = CrudState.ERROR;
            }
            finally
            {
                CloseConnection();
            }

            return item;
        }

        public override LaboDataViscosityColDto Update(LaboDataViscosityColDto data)
        {
            throw new NotImplementedException();
        }
    }
}
