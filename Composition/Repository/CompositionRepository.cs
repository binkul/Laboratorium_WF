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

namespace Laboratorium.Composition.Repository
{
    public class CompositionRepository : BasicCRUD<CompositionDto>
    {
        private const string SQL_CMB_MATERIAL = "Select mat.[name], mat.id, CAST(is_intermediate as bit) as [intermediate], ISNULL(VOC, -1) as VOC, ISNULL(mat.price, -1) as price, ISNULL(mat.price * cur.rate, -1) as price_pl, " +
            "cur.currency, cur.rate From Konkurencja.dbo.Material mat Left Join Konkurencja.dbo.CmbCurrency cur on mat.currency_id=cur.id Where mat.is_package='false' Union All Select [name], " +
            "labo_id, CAST(1 as bit) as [intermediate], -1 as VOC, -1 as price, -1 as price_pl, 'Zł' as currency, 1 as rate From Konkurencja.dbo.LaboSemiProduct Where is_active='true' Order by intermediate, [name]";

        private static readonly SqlIndex SQL_INDEX = SqlIndex.CompositionIndex;
        private static readonly string TABLE_NAME = Table.COMPOSITION_TABLE;
        private readonly IService _service;

        public CompositionRepository(SqlConnection connection, IService service) : base(connection, SQL_INDEX, TABLE_NAME)
        {
            _service = service;
        }

        public IList<CmbMaterialCompositionDto> GetCmbMaterials()
        {
            IList<CmbMaterialCompositionDto> list = new List<CmbMaterialCompositionDto>();

            try
            {
                SqlCommand command = new SqlCommand(SQL_CMB_MATERIAL, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    int id = 0;
                    while (reader.Read())
                    {
                        id++;
                        string material = reader.GetString(0);
                        int materialId = reader.GetInt32(1);
                        bool semiprod = reader.GetBoolean(2);
                        double voc = reader.GetDouble(3);
                        double price = reader.GetDouble(4);
                        double pricePl = reader.GetDouble(5);
                        string currency = CommonFunction.DBNullToStringConv(reader.GetValue(6));
                        double rate = reader.GetDouble(7);

                        CmbMaterialCompositionDto mat = new CmbMaterialCompositionDto(id, material, materialId, semiprod, voc, price, pricePl, currency, rate);

                        list.Add(mat);
                    }
                    reader.Close();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu GetMaterialCompositionDto ",
                    "Błąd połaczenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd systemowy w czasie operacji na tabeli 'Material'" + ex.Message + "'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }


            return list;
        }

        public override IList<CompositionDto> GetAllByLaboId(int laboId)
        {
            IList<CompositionDto> list = new List<CompositionDto>();

            try
            {
                string query = SqlRead.ReadByName[_sqlIndex].Replace("XXXX", laboId.ToString());
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int labo = reader.GetInt32(0);
                        int version = reader.GetInt32(1);
                        short ordering = reader.GetInt16(2);
                        string material = reader.GetString(3);
                        int materialId = reader.GetInt32(4);
                        bool semiprod = reader.GetBoolean(5);
                        double amount = reader.GetDouble(6);
                        byte operation = reader.GetByte(7);
                        string comment = CommonFunction.DBNullToStringConv(reader.GetValue(8));
                        double? voc = CommonFunction.DBNullToDoubleConv(reader.GetValue(9));
                        double? priceOrg = CommonFunction.DBNullToDoubleConv(reader.GetValue(10));
                        string currency = CommonFunction.DBNullToStringConv(reader.GetValue(11));
                        double? rate = CommonFunction.DBNullToDoubleConv(reader.GetValue(12));
                        double? pricePl = CommonFunction.DBNullToDoubleConv(reader.GetValue(13));

                        CompositionDto composition = new CompositionDto.Builder()
                            .LaboId(labo)
                            .Version(version)
                            .Ordering(ordering)
                            .Material(material)
                            .MaterialId(materialId)
                            .IsSemiproduct(semiprod)
                            .Amount(amount)
                            .VOC(voc)
                            .Operation(operation)
                            .Comment(comment)
                            .PricePl(pricePl)
                            .PriceOriginal(priceOrg)
                            .Currency(currency)
                            .Rate(rate)
                            .Service(_service)
                            .Build();

                        composition.AcceptChanges();
                        list.Add(composition);
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

        public override IList<CompositionDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public override CompositionDto Save(CompositionDto data)
        {
            throw new NotImplementedException();
        }

        public override CompositionDto Update(CompositionDto data)
        {
            throw new NotImplementedException();
        }
    }
}
