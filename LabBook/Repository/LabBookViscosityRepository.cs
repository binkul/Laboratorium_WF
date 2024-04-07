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
    public class LabBookViscosityRepository : ExtendedCRUD<LaboDataViscosityDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.LaboViscosityIndex;
        private static readonly string TABLE_NAME = Table.LABO_VISC_DATA_TABLE;
        private readonly IService _service;

        public LabBookViscosityRepository(SqlConnection connection, IService service) : base(connection, SQL_INDEX, TABLE_NAME)
        {
            _service = service;
        }

        public override IList<LaboDataViscosityDto> GetAll()
        {
            List<LaboDataViscosityDto> list = new List<LaboDataViscosityDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(1);
                        int title = reader.GetInt32(2);
                        bool toCompare = reader.GetBoolean(3);
                        double? pH = CommonFunction.DBNullToDoubleConv(reader.GetValue(4));
                        string temp = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        double? brook1 = CommonFunction.DBNullToDoubleConv(reader.GetValue(6));
                        double? brook5 = CommonFunction.DBNullToDoubleConv(reader.GetValue(7));
                        double? brook10 = CommonFunction.DBNullToDoubleConv(reader.GetValue(8));
                        double? brook20 = CommonFunction.DBNullToDoubleConv(reader.GetValue(9));
                        double? brook30 = CommonFunction.DBNullToDoubleConv(reader.GetValue(10));
                        double? brook40 = CommonFunction.DBNullToDoubleConv(reader.GetValue(11));
                        double? brook50 = CommonFunction.DBNullToDoubleConv(reader.GetValue(12));
                        double? brook60 = CommonFunction.DBNullToDoubleConv(reader.GetValue(13));
                        double? brook70 = CommonFunction.DBNullToDoubleConv(reader.GetValue(14));
                        double? brook80 = CommonFunction.DBNullToDoubleConv(reader.GetValue(15));
                        double? brook90 = CommonFunction.DBNullToDoubleConv(reader.GetValue(16));
                        double? brook100 = CommonFunction.DBNullToDoubleConv(reader.GetValue(17));
                        string brookDisc = CommonFunction.DBNullToStringConv(reader.GetValue(18));
                        string brookComment = CommonFunction.DBNullToStringConv(reader.GetValue(19));
                        double? brookXvis = CommonFunction.DBNullToDoubleConv(reader.GetValue(20));
                        string brookXrpm = CommonFunction.DBNullToStringConv(reader.GetValue(21));
                        string brookXdisc = CommonFunction.DBNullToStringConv(reader.GetValue(22));
                        double? krebs = CommonFunction.DBNullToDoubleConv(reader.GetValue(23));
                        string krebsComment = CommonFunction.DBNullToStringConv(reader.GetValue(24));
                        double? ici = CommonFunction.DBNullToDoubleConv(reader.GetValue(25));
                        string iciDisc = CommonFunction.DBNullToStringConv(reader.GetValue(26));
                        string iciComment = CommonFunction.DBNullToStringConv(reader.GetValue(27));
                        DateTime dateCreated = reader.GetDateTime(2);
                        DateTime dateUpdated = !reader.GetValue(3).Equals(DBNull.Value) ? reader.GetDateTime(3) : dateCreated;

                        LaboDataViscosityDto labo = new LaboDataViscosityDto.Builder()
                            .ToCompare(toCompare)
                            .pH(pH)
                            .Temp(temp)
                            .Brook1(brook1)
                            .Brook5(brook5)
                            .Brook10(brook10)
                            .Brook20(brook20)
                            .Brook30(brook30)
                            .Brook40(brook40)
                            .Brook50(brook50)
                            .Brook60(brook60)
                            .Brook70(brook60)
                            .Brook80(brook80)
                            .Brook90(brook80)
                            .Brook100(brook100)
                            .BrookDisc(brookDisc)
                            .BrookComment(brookComment)
                            .BrookXviscosity(brookXvis)
                            .BrookXrpm(brookXrpm)
                            .BrookXdisc(brookDisc)
                            .Krebs(krebs)
                            .KrebsComment(krebsComment)
                            .ICI(ici)
                            .IciDisc(iciDisc)
                            .IciComment(iciComment)
                            .DateCreated(dateCreated)
                            .DateUpdated(dateUpdated)
                            .Service(_service)
                            .Build();

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

        public override IList<LaboDataViscosityDto> GetAllByLaboId(int laboId)
        {
            List<LaboDataViscosityDto> list = new List<LaboDataViscosityDto>();

            try
            {
                string sql = SqlRead.ReadByName[_sqlIndex].Replace("XXXX", laboId.ToString());
                SqlCommand command = new SqlCommand(sql, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(1);
                        int labId = reader.GetInt32(2);
                        bool toCompare = reader.GetBoolean(3);
                        double? pH = CommonFunction.DBNullToDoubleConv(reader.GetValue(4));
                        string temp = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        double? brook1 = CommonFunction.DBNullToDoubleConv(reader.GetValue(6));
                        double? brook5 = CommonFunction.DBNullToDoubleConv(reader.GetValue(7));
                        double? brook10 = CommonFunction.DBNullToDoubleConv(reader.GetValue(8));
                        double? brook20 = CommonFunction.DBNullToDoubleConv(reader.GetValue(9));
                        double? brook30 = CommonFunction.DBNullToDoubleConv(reader.GetValue(10));
                        double? brook40 = CommonFunction.DBNullToDoubleConv(reader.GetValue(11));
                        double? brook50 = CommonFunction.DBNullToDoubleConv(reader.GetValue(12));
                        double? brook60 = CommonFunction.DBNullToDoubleConv(reader.GetValue(13));
                        double? brook70 = CommonFunction.DBNullToDoubleConv(reader.GetValue(14));
                        double? brook80 = CommonFunction.DBNullToDoubleConv(reader.GetValue(15));
                        double? brook90 = CommonFunction.DBNullToDoubleConv(reader.GetValue(16));
                        double? brook100 = CommonFunction.DBNullToDoubleConv(reader.GetValue(17));
                        string brookDisc = CommonFunction.DBNullToStringConv(reader.GetValue(18));
                        string brookComment = CommonFunction.DBNullToStringConv(reader.GetValue(19));
                        double? brookXvis = CommonFunction.DBNullToDoubleConv(reader.GetValue(20));
                        string brookXrpm = CommonFunction.DBNullToStringConv(reader.GetValue(21));
                        string brookXdisc = CommonFunction.DBNullToStringConv(reader.GetValue(22));
                        double? krebs = CommonFunction.DBNullToDoubleConv(reader.GetValue(23));
                        string krebsComment = CommonFunction.DBNullToStringConv(reader.GetValue(24));
                        double? ici = CommonFunction.DBNullToDoubleConv(reader.GetValue(25));
                        string iciDisc = CommonFunction.DBNullToStringConv(reader.GetValue(26));
                        string iciComment = CommonFunction.DBNullToStringConv(reader.GetValue(27));
                        DateTime dateCreated = reader.GetDateTime(28);
                        DateTime dateUpdated = !reader.GetValue(29).Equals(DBNull.Value) ? reader.GetDateTime(29) : dateCreated;

                        LaboDataViscosityDto labo = new LaboDataViscosityDto.Builder()
                            .Id(id)
                            .LaboId(laboId)
                            .ToCompare(toCompare)
                            .pH(pH)
                            .Temp(temp)
                            .Brook1(brook1)
                            .Brook5(brook5)
                            .Brook10(brook10)
                            .Brook20(brook20)
                            .Brook30(brook30)
                            .Brook40(brook40)
                            .Brook50(brook50)
                            .Brook60(brook60)
                            .Brook70(brook60)
                            .Brook80(brook80)
                            .Brook90(brook80)
                            .Brook100(brook100)
                            .BrookDisc(brookDisc)
                            .BrookComment(brookComment)
                            .BrookXviscosity(brookXvis)
                            .BrookXrpm(brookXrpm)
                            .BrookXdisc(brookDisc)
                            .Krebs(krebs)
                            .KrebsComment(krebsComment)
                            .ICI(ici)
                            .IciDisc(iciDisc)
                            .IciComment(iciComment)
                            .DateCreated(dateCreated)
                            .DateUpdated(dateUpdated)
                            .Service(_service)
                            .Build();

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

        public override LaboDataViscosityDto Save(LaboDataViscosityDto data)
        {
            LaboDataViscosityDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@labo_id", item.LaboId);
                command.Parameters.AddWithValue("@to_compare", item.ToCompare);
                command.Parameters.AddWithValue("@pH", CommonFunction.NullDoubleToDBNullConv(item.pH));
                command.Parameters.AddWithValue("@temp", CommonFunction.NullStringToDBNullConv(item.Temp));
                command.Parameters.AddWithValue("@brook_1", CommonFunction.NullDoubleToDBNullConv(item.Brook1));
                command.Parameters.AddWithValue("@brook_5", CommonFunction.NullDoubleToDBNullConv(item.Brook5));
                command.Parameters.AddWithValue("@brook_10", CommonFunction.NullDoubleToDBNullConv(item.Brook10));
                command.Parameters.AddWithValue("@brook_20", CommonFunction.NullDoubleToDBNullConv(item.Brook20));
                command.Parameters.AddWithValue("@brook_30", CommonFunction.NullDoubleToDBNullConv(item.Brook30));
                command.Parameters.AddWithValue("@brook_40", CommonFunction.NullDoubleToDBNullConv(item.Brook40));
                command.Parameters.AddWithValue("@brook_50", CommonFunction.NullDoubleToDBNullConv(item.Brook50));
                command.Parameters.AddWithValue("@brook_60", CommonFunction.NullDoubleToDBNullConv(item.Brook60));
                command.Parameters.AddWithValue("@brook_70", CommonFunction.NullDoubleToDBNullConv(item.Brook70));
                command.Parameters.AddWithValue("@brook_80", CommonFunction.NullDoubleToDBNullConv(item.Brook80));
                command.Parameters.AddWithValue("@brook_90", CommonFunction.NullDoubleToDBNullConv(item.Brook90));
                command.Parameters.AddWithValue("@brook_100", CommonFunction.NullDoubleToDBNullConv(item.Brook100));
                command.Parameters.AddWithValue("@brook_disc", CommonFunction.NullStringToDBNullConv(item.BrookDisc));
                command.Parameters.AddWithValue("@brook_comment", CommonFunction.NullStringToDBNullConv(item.BrookComment));
                command.Parameters.AddWithValue("@brook_x_vis", CommonFunction.NullDoubleToDBNullConv(item.BrookXvisc));
                command.Parameters.AddWithValue("@brook_x_rpm", CommonFunction.NullStringToDBNullConv(item.BrookXrpm));
                command.Parameters.AddWithValue("@brook_x_disc", CommonFunction.NullStringToDBNullConv(item.BrookXdisc));
                command.Parameters.AddWithValue("@krebs", CommonFunction.NullDoubleToDBNullConv(item.Krebs));
                command.Parameters.AddWithValue("@krebs_comment", CommonFunction.NullStringToDBNullConv(item.KrebsComment));
                command.Parameters.AddWithValue("@ici", CommonFunction.NullDoubleToDBNullConv(item.ICI));
                command.Parameters.AddWithValue("@ici_disc", CommonFunction.NullStringToDBNullConv(item.IciDisc));
                command.Parameters.AddWithValue("@ici_comment", CommonFunction.NullStringToDBNullConv(item.IciComment));
                command.Parameters.AddWithValue("@date_created", item.DateCreated);
                command.Parameters.AddWithValue("@date_updated", item.DateUpdated);
                OpenConnection();
                short id = Convert.ToInt16(command.ExecuteScalar());
                item.Id = id;
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

        public override LaboDataViscosityDto Update(LaboDataViscosityDto data)
        {
            LaboDataViscosityDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlUpdate.Update[_sqlIndex];
                command.Parameters.AddWithValue("@to_compare", item.ToCompare);
                command.Parameters.AddWithValue("@pH", CommonFunction.NullDoubleToDBNullConv(item.pH));
                command.Parameters.AddWithValue("@temp", CommonFunction.NullStringToDBNullConv(item.Temp));
                command.Parameters.AddWithValue("@brook_1", CommonFunction.NullDoubleToDBNullConv(item.Brook1));
                command.Parameters.AddWithValue("@brook_5", CommonFunction.NullDoubleToDBNullConv(item.Brook5));
                command.Parameters.AddWithValue("@brook_10", CommonFunction.NullDoubleToDBNullConv(item.Brook10));
                command.Parameters.AddWithValue("@brook_20", CommonFunction.NullDoubleToDBNullConv(item.Brook20));
                command.Parameters.AddWithValue("@brook_30", CommonFunction.NullDoubleToDBNullConv(item.Brook30));
                command.Parameters.AddWithValue("@brook_40", CommonFunction.NullDoubleToDBNullConv(item.Brook40));
                command.Parameters.AddWithValue("@brook_50", CommonFunction.NullDoubleToDBNullConv(item.Brook50));
                command.Parameters.AddWithValue("@brook_60", CommonFunction.NullDoubleToDBNullConv(item.Brook60));
                command.Parameters.AddWithValue("@brook_70", CommonFunction.NullDoubleToDBNullConv(item.Brook70));
                command.Parameters.AddWithValue("@brook_80", CommonFunction.NullDoubleToDBNullConv(item.Brook80));
                command.Parameters.AddWithValue("@brook_90", CommonFunction.NullDoubleToDBNullConv(item.Brook90));
                command.Parameters.AddWithValue("@brook_100", CommonFunction.NullDoubleToDBNullConv(item.Brook100));
                command.Parameters.AddWithValue("@brook_disc", CommonFunction.NullStringToDBNullConv(item.BrookDisc));
                command.Parameters.AddWithValue("@brook_comment", CommonFunction.NullStringToDBNullConv(item.BrookComment));
                command.Parameters.AddWithValue("@brook_x_vis", CommonFunction.NullDoubleToDBNullConv(item.BrookXvisc));
                command.Parameters.AddWithValue("@brook_x_rpm", CommonFunction.NullStringToDBNullConv(item.BrookXrpm));
                command.Parameters.AddWithValue("@brook_x_disc", CommonFunction.NullStringToDBNullConv(item.BrookXdisc));
                command.Parameters.AddWithValue("@krebs", CommonFunction.NullDoubleToDBNullConv(item.Krebs));
                command.Parameters.AddWithValue("@krebs_comment", CommonFunction.NullStringToDBNullConv(item.KrebsComment));
                command.Parameters.AddWithValue("@ici", CommonFunction.NullDoubleToDBNullConv(item.ICI));
                command.Parameters.AddWithValue("@ici_disc", CommonFunction.NullStringToDBNullConv(item.IciDisc));
                command.Parameters.AddWithValue("@ici_comment", CommonFunction.NullStringToDBNullConv(item.IciComment));
                command.Parameters.AddWithValue("@date_created", item.DateCreated);
                command.Parameters.AddWithValue("@date_updated", item.DateUpdated);
                command.Parameters.AddWithValue("@id", item.Id);
                OpenConnection();
                command.ExecuteNonQuery();
                item.CrudState = CrudState.OK;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Update " + _tableName,
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

        public override void UpdateRow(DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
