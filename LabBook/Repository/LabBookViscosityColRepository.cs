﻿using Laboratorium.ADO;
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

        public LaboDataViscosityColDto GetByLaboId(int laboId)
        {
            LaboDataViscosityColDto col = null;

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
                            col = new LaboDataViscosityColDto(id, profile, columns);
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

            return col;
        }

        public override IList<LaboDataViscosityColDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public override LaboDataViscosityColDto Save(LaboDataViscosityColDto data)
        {
            throw new NotImplementedException();
        }

        public override LaboDataViscosityColDto Update(LaboDataViscosityColDto data)
        {
            throw new NotImplementedException();
        }
    }
}