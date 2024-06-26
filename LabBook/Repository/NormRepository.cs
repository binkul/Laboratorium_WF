﻿using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Repository
{
    public class NormRepository : BasicCRUD<NormDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.NormIndex;
        private static readonly string TABLE_NAME = Table.NORM_TABLE;

        public NormRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<NormDto> GetAll()
        {
            List<NormDto> list = new List<NormDto>();

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
                        string namePl = CommonFunction.DBNullToStringConv(reader.GetValue(1));
                        string nameEn = CommonFunction.DBNullToStringConv(reader.GetValue(2));
                        string descrip = CommonFunction.DBNullToStringConv(reader.GetValue(3));
                        byte position = reader.GetByte(4);
                        string groupName = CommonFunction.DBNullToStringConv(reader.GetValue(5));
                        byte groupId = reader.GetByte(6);

                        NormDto norm = new NormDto(id, namePl, nameEn, descrip, position, groupName, groupId);
                        list.Add(norm);
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

        public override NormDto Save(NormDto data)
        {
            throw new NotImplementedException();
        }

        public override NormDto Update(NormDto data)
        {
            throw new NotImplementedException();
        }
    }
}
