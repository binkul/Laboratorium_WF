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

namespace Laboratorium.Material.Repository
{
    public class MaterialRepository : ExtendedCRUD<MaterialDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.MaterialIndex;
        private static readonly string TABLE_NAME = Table.MATERIAL_TABLE;
        private readonly IService _service;

        public MaterialRepository(SqlConnection connection, IService service) : base(connection, SQL_INDEX, TABLE_NAME)
        {
            _service = service;
        }

        public override IList<MaterialDto> GetAll()
        {
            List<MaterialDto> list = new List<MaterialDto>();

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
                        string name = reader.GetString(1);
                        string index = CommonFunction.DBNullToStringConv(reader.GetValue(2));
                        byte suppId = reader.GetByte(3);
                        short functionId = reader.GetInt16(4);
                        bool isInt = reader.GetBoolean(5);
                        bool isDen = reader.GetBoolean(6);
                        bool isProd = reader.GetBoolean(7);
                        bool isObs = reader.GetBoolean(8);
                        bool isAct = reader.GetBoolean(9);
                        bool isPack = reader.GetBoolean(10);
                        double? price = CommonFunction.DBNullToDoubleConv(reader.GetValue(11));
                        double? pricePQ = CommonFunction.DBNullToDoubleConv(reader.GetValue(12));
                        double? priceT = CommonFunction.DBNullToDoubleConv(reader.GetValue(13));
                        double? Quantity = CommonFunction.DBNullToDoubleConv(reader.GetValue(14));
                        byte currencyId = reader.GetByte(15);
                        byte unitId = reader.GetByte(16);
                        string priceUnit = CommonFunction.DBNullToStringConv(reader.GetValue(17));
                        double? density = CommonFunction.DBNullToDoubleConv(reader.GetValue(18));
                        double? solids = CommonFunction.DBNullToDoubleConv(reader.GetValue(19));
                        double? ash = CommonFunction.DBNullToDoubleConv(reader.GetValue(20));
                        double? voc = CommonFunction.DBNullToDoubleConv(reader.GetValue(21));
                        string vocProc = CommonFunction.DBNullToStringConv(reader.GetValue(22));
                        string remarks = CommonFunction.DBNullToStringConv(reader.GetValue(23));
                        DateTime dateCreated = reader.GetDateTime(24);
                        DateTime dateUpdated = !reader.GetValue(11).Equals(DBNull.Value) ? reader.GetDateTime(25) : dateCreated;

                        MaterialDto material = new MaterialDto.Builder()
                            .Id(id)
                            .Name(name)
                            .Index(index)
                            .SupplierId(suppId)
                            .FunctinoId(functionId)
                            .IsIntermediate(isInt)
                            .Isdanger(isDen)
                            .IsProduction(isProd)
                            .IsObserved(isObs)
                            .IsActive(isAct)
                            .IsPackage(isPack)
                            .Price(price)
                            .PricePerQuantity(pricePQ)
                            .PriceTransport(priceT)
                            .Quantity(Quantity)
                            .CurrencyId(currencyId)
                            .UnitId(unitId)
                            .PriceUnit(priceUnit)
                            .Density(density)
                            .Solids(solids)
                            .Ash450(ash)
                            .VOC(voc)
                            .VocPercent(vocProc)
                            .Remarks(remarks)
                            .DateCreated(dateCreated)
                            .DateUpdated(dateUpdated)
                            .Service(_service)
                            .Build();

                        material.AcceptChanges();
                        list.Add(material);
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

        public override MaterialDto Save(MaterialDto data)
        {
            MaterialDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlSave.Save[_sqlIndex];
                command.Parameters.AddWithValue("@name", item.Name);
                command.Parameters.AddWithValue("@index_db", CommonFunction.NullStringToDBNullConv(item.Index));
                command.Parameters.AddWithValue("@supplier_id", item.SupplierId);
                command.Parameters.AddWithValue("@function_id", item.FunctionId);
                command.Parameters.AddWithValue("@is_intermediate", item.IsIntermediate);
                command.Parameters.AddWithValue("@is_danger", item.IsDanger);
                command.Parameters.AddWithValue("@is_production", item.IsProduction);
                command.Parameters.AddWithValue("@is_observed", item.IsObserved);
                command.Parameters.AddWithValue("@is_active", item.IsActive);
                command.Parameters.AddWithValue("@is_package", item.IsPackage);
                command.Parameters.AddWithValue("@price", CommonFunction.NullDoubleToDBNullConv(item.Price));
                command.Parameters.AddWithValue("@price_per_quantity", CommonFunction.NullDoubleToDBNullConv(item.PricePerQuantity));
                command.Parameters.AddWithValue("@price_transport", CommonFunction.NullDoubleToDBNullConv(item.PriceTransport));
                command.Parameters.AddWithValue("@quantity", CommonFunction.NullDoubleToDBNullConv(item.Quantity));
                command.Parameters.AddWithValue("@currency_id", item.CurrencyId);
                command.Parameters.AddWithValue("@unit_id", item.UnitId);
                command.Parameters.AddWithValue("@density", CommonFunction.NullDoubleToDBNullConv(item.Density));
                command.Parameters.AddWithValue("@solids", CommonFunction.NullDoubleToDBNullConv(item.Solids));
                command.Parameters.AddWithValue("@ash_450", CommonFunction.NullDoubleToDBNullConv(item.Ash450));
                command.Parameters.AddWithValue("@VOC", CommonFunction.NullDoubleToDBNullConv(item.VOC));
                command.Parameters.AddWithValue("@remarks", CommonFunction.NullStringToDBNullConv(item.Remarks));
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

        public override MaterialDto Update(MaterialDto data)
        {
            MaterialDto item = data;

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = SqlUpdate.Update[_sqlIndex];
                command.Parameters.AddWithValue("@name", item.Name);
                command.Parameters.AddWithValue("@index_db", CommonFunction.NullStringToDBNullConv(item.Index));
                command.Parameters.AddWithValue("@supplier_id", item.SupplierId);
                command.Parameters.AddWithValue("@function_id", item.FunctionId);
                command.Parameters.AddWithValue("@is_intermediate", item.IsIntermediate);
                command.Parameters.AddWithValue("@is_danger", item.IsDanger);
                command.Parameters.AddWithValue("@is_production", item.IsProduction);
                command.Parameters.AddWithValue("@is_observed", item.IsObserved);
                command.Parameters.AddWithValue("@is_active", item.IsActive);
                command.Parameters.AddWithValue("@is_package", item.IsPackage);
                command.Parameters.AddWithValue("@price", CommonFunction.NullDoubleToDBNullConv(item.Price));
                command.Parameters.AddWithValue("@price_per_quantity", CommonFunction.NullDoubleToDBNullConv(item.PricePerQuantity));
                command.Parameters.AddWithValue("@price_transport", CommonFunction.NullDoubleToDBNullConv(item.PriceTransport));
                command.Parameters.AddWithValue("@quantity", CommonFunction.NullDoubleToDBNullConv(item.Quantity));
                command.Parameters.AddWithValue("@currency_id", item.CurrencyId);
                command.Parameters.AddWithValue("@unit_id", item.UnitId);
                command.Parameters.AddWithValue("@density", CommonFunction.NullDoubleToDBNullConv(item.Density));
                command.Parameters.AddWithValue("@solids", CommonFunction.NullDoubleToDBNullConv(item.Solids));
                command.Parameters.AddWithValue("@ash_450", CommonFunction.NullDoubleToDBNullConv(item.Ash450));
                command.Parameters.AddWithValue("@VOC", CommonFunction.NullDoubleToDBNullConv(item.VOC));
                command.Parameters.AddWithValue("@remarks", CommonFunction.NullStringToDBNullConv(item.Remarks));
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
