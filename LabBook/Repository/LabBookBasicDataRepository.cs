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
    public class LabBookBasicDataRepository : BasicCRUD<LaboDataBasicDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.LaboBasicIndex;
        private static readonly string TABLE_NAME = Table.LABO_BASIC_DATA_TABLE;
        private readonly IService _service;

        public LabBookBasicDataRepository(SqlConnection connection, IService service) : base(connection, SQL_INDEX, TABLE_NAME)
        {
            _service = service;
        }

        public override IList<LaboDataBasicDto> GetAll()
        {
            List<LaboDataBasicDto> list = new List<LaboDataBasicDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int laboId = reader.GetInt32(1);
                        double? gloss20 = CommonFunction.DBNullToDoubleConv(reader.GetValue(2));
                        double? gloss60 = CommonFunction.DBNullToDoubleConv(reader.GetValue(3));
                        double? gloss85 = CommonFunction.DBNullToDoubleConv(reader.GetValue(4));
                        byte glossClass = reader.GetByte(5);
                        string glossComm = CommonFunction.DBNullToStringConv(reader.GetValue(6));
                        string scrubBrush = CommonFunction.DBNullToStringConv(reader.GetValue(7));
                        string scrubsponge = CommonFunction.DBNullToStringConv(reader.GetValue(8));
                        byte scrubClass = reader.GetByte(9);
                        string scrubComm = CommonFunction.DBNullToStringConv(reader.GetValue(10));
                        byte contrastClass = reader.GetByte(11);
                        string contrastComm = CommonFunction.DBNullToStringConv(reader.GetValue(12));
                        string voc = CommonFunction.DBNullToStringConv(reader.GetValue(13));
                        byte vocClass = reader.GetByte(14);
                        string yield = CommonFunction.DBNullToStringConv(reader.GetValue(15));
                        string yieldFormula = CommonFunction.DBNullToStringConv(reader.GetValue(16));
                        string adhesion = CommonFunction.DBNullToStringConv(reader.GetValue(17));
                        string flow = CommonFunction.DBNullToStringConv(reader.GetValue(18));
                        string spill = CommonFunction.DBNullToStringConv(reader.GetValue(19));
                        string dryI = CommonFunction.DBNullToStringConv(reader.GetValue(20));
                        string dryII = CommonFunction.DBNullToStringConv(reader.GetValue(21));
                        string dryIII = CommonFunction.DBNullToStringConv(reader.GetValue(22));
                        string dryIV = CommonFunction.DBNullToStringConv(reader.GetValue(23));
                        string dryV = CommonFunction.DBNullToStringConv(reader.GetValue(24));
                        DateTime dateUpdated = !reader.GetValue(3).Equals(DBNull.Value) ? reader.GetDateTime(25) : DateTime.Today;

                        LaboDataBasicDto labo = new LaboDataBasicDto.Builder()
                            .Id(id)
                            .LaboId(laboId)
                            .Gloss20(gloss20)
                            .Gloss60(gloss60)
                            .Gloss85(gloss85)
                            .GlossClassId(glossClass)
                            .GlossComment(glossComm)
                            .ScrubBrush(scrubBrush)
                            .ScrubSponge(scrubsponge)
                            .ScrubClassId(scrubClass)
                            .ScrubComment(scrubComm)
                            .ContrastClassId(contrastClass)
                            .ContrastComment(contrastComm)
                            .VOC(voc)
                            .VocClassId(vocClass)
                            .Yield(yield)
                            .YieldFormula(yieldFormula)
                            .Adhesion(adhesion)
                            .Flow(flow)
                            .Spill(spill)
                            .DryingI(dryI)
                            .DryingII(dryII)
                            .DryingIII(dryIII)
                            .DryingIV(dryIV)
                            .DryingV(dryV)
                            .Date(dateUpdated)
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

        public override LaboDataBasicDto Save(LaboDataBasicDto data)
        {
            LaboDataBasicDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@labo_id", item.LaboId);
                command.Parameters.AddWithValue("@gloss_20", CommonFunction.NullDoubleToDBNullConv(item.Gloss20));
                command.Parameters.AddWithValue("@gloss_60", CommonFunction.NullDoubleToDBNullConv(item.Gloss60));
                command.Parameters.AddWithValue("@gloss_85", CommonFunction.NullDoubleToDBNullConv(item.Gloss85));
                command.Parameters.AddWithValue("@gloss_class", item.GlossClassId);
                command.Parameters.AddWithValue("@gloss_comment", CommonFunction.NullStringToDBNullConv(item.GlossComment));
                command.Parameters.AddWithValue("@scrub_brush", CommonFunction.NullStringToDBNullConv(item.ScrubBrush));
                command.Parameters.AddWithValue("@scrub_sponge", CommonFunction.NullStringToDBNullConv(item.ScrubSponge));
                command.Parameters.AddWithValue("@scrub_class", item.ScrubClassId);
                command.Parameters.AddWithValue("@scrub_comment", CommonFunction.NullStringToDBNullConv(item.ScrubComment));
                command.Parameters.AddWithValue("@contrast_class", item.ContrastClassId);
                command.Parameters.AddWithValue("@contrast_comment", CommonFunction.NullStringToDBNullConv(item.ContrastComment));
                command.Parameters.AddWithValue("@voc", CommonFunction.NullStringToDBNullConv(item.VOC));
                command.Parameters.AddWithValue("@voc_class", item.VocClassId);
                command.Parameters.AddWithValue("@yield", CommonFunction.NullStringToDBNullConv(item.Yield));
                command.Parameters.AddWithValue("@yield_formula", CommonFunction.NullStringToDBNullConv(item.YieldFormula));
                command.Parameters.AddWithValue("@adhesion", CommonFunction.NullStringToDBNullConv(item.Adhesion));
                command.Parameters.AddWithValue("@flow", CommonFunction.NullStringToDBNullConv(item.Flow));
                command.Parameters.AddWithValue("@spill", CommonFunction.NullStringToDBNullConv(item.Spill));
                command.Parameters.AddWithValue("@drying_I", CommonFunction.NullStringToDBNullConv(item.DryingI));
                command.Parameters.AddWithValue("@drying_II", CommonFunction.NullStringToDBNullConv(item.DryingII));
                command.Parameters.AddWithValue("@drying_III", CommonFunction.NullStringToDBNullConv(item.DryingIII));
                command.Parameters.AddWithValue("@drying_IV", CommonFunction.NullStringToDBNullConv(item.DryingIV));
                command.Parameters.AddWithValue("@drying_V", CommonFunction.NullStringToDBNullConv(item.DryingV));
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

        public override LaboDataBasicDto Update(LaboDataBasicDto data)
        {
            LaboDataBasicDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlUpdate.Update[_sqlIndex];
                command.Parameters.AddWithValue("@gloss_20", CommonFunction.NullDoubleToDBNullConv(item.Gloss20));
                command.Parameters.AddWithValue("@gloss_60", CommonFunction.NullDoubleToDBNullConv(item.Gloss60));
                command.Parameters.AddWithValue("@gloss_85", CommonFunction.NullDoubleToDBNullConv(item.Gloss85));
                command.Parameters.AddWithValue("@gloss_class", item.GlossClassId);
                command.Parameters.AddWithValue("@gloss_comment", CommonFunction.NullStringToDBNullConv(item.GlossComment));
                command.Parameters.AddWithValue("@scrub_brush", CommonFunction.NullStringToDBNullConv(item.ScrubBrush));
                command.Parameters.AddWithValue("@scrub_sponge", CommonFunction.NullStringToDBNullConv(item.ScrubSponge));
                command.Parameters.AddWithValue("@scrub_class", item.ScrubClassId);
                command.Parameters.AddWithValue("@scrub_comment", CommonFunction.NullStringToDBNullConv(item.ScrubComment));
                command.Parameters.AddWithValue("@contrast_class", item.ContrastClassId);
                command.Parameters.AddWithValue("@contrast_comment", CommonFunction.NullStringToDBNullConv(item.ContrastComment));
                command.Parameters.AddWithValue("@voc", CommonFunction.NullStringToDBNullConv(item.VOC));
                command.Parameters.AddWithValue("@voc_class", item.VocClassId);
                command.Parameters.AddWithValue("@yield", CommonFunction.NullStringToDBNullConv(item.Yield));
                command.Parameters.AddWithValue("@yield_formula", CommonFunction.NullStringToDBNullConv(item.YieldFormula));
                command.Parameters.AddWithValue("@adhesion", CommonFunction.NullStringToDBNullConv(item.Adhesion));
                command.Parameters.AddWithValue("@flow", CommonFunction.NullStringToDBNullConv(item.Flow));
                command.Parameters.AddWithValue("@spill", CommonFunction.NullStringToDBNullConv(item.Spill));
                command.Parameters.AddWithValue("@drying_I", CommonFunction.NullStringToDBNullConv(item.DryingI));
                command.Parameters.AddWithValue("@drying_II", CommonFunction.NullStringToDBNullConv(item.DryingII));
                command.Parameters.AddWithValue("@drying_III", CommonFunction.NullStringToDBNullConv(item.DryingIII));
                command.Parameters.AddWithValue("@drying_IV", CommonFunction.NullStringToDBNullConv(item.DryingIV));
                command.Parameters.AddWithValue("@drying_V", CommonFunction.NullStringToDBNullConv(item.DryingV));
                command.Parameters.AddWithValue("@date_updated", item.DateUpdated);
                command.Parameters.AddWithValue("@labo_id", item.LaboId);
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
    }
}
