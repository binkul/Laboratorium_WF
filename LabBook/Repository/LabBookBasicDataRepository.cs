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
                        string adhesion = CommonFunction.DBNullToStringConv(reader.GetValue(16));
                        string flow = CommonFunction.DBNullToStringConv(reader.GetValue(17));
                        string spill = CommonFunction.DBNullToStringConv(reader.GetValue(18));
                        string dryI = CommonFunction.DBNullToStringConv(reader.GetValue(19));
                        string dryII = CommonFunction.DBNullToStringConv(reader.GetValue(20));
                        string dryIII = CommonFunction.DBNullToStringConv(reader.GetValue(21));
                        string dryIV = CommonFunction.DBNullToStringConv(reader.GetValue(22));
                        string dryV = CommonFunction.DBNullToStringConv(reader.GetValue(23));
                        DateTime dateUpdated = !reader.GetValue(3).Equals(DBNull.Value) ? reader.GetDateTime(24) : DateTime.Today;

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
            throw new NotImplementedException();
        }

        public override LaboDataBasicDto Update(LaboDataBasicDto data)
        {
            throw new NotImplementedException();
        }
    }
}
