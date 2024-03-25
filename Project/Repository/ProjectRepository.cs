using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Project.Repository
{
    public class ProjectRepository : BasicCRUD<ProjectDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.ProjectIndex;
        private static readonly string TABLE_NAME = Table.PROJECT_TABLE;

        public ProjectRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<ProjectDto> GetAll()
        {
            List<ProjectDto> list = new List<ProjectDto>();

            try
            {
                SqlCommand command = new SqlCommand(SqlRead.Read[_sqlIndex], _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string title = reader.GetString(1);
                        string comments = CommonFunction.DBNullToStringConv(reader.GetValue(2));
                        bool archive = reader.GetBoolean(3);
                        bool labo = reader.GetBoolean(4);
                        bool auction = reader.GetBoolean(5);
                        string disc = CommonFunction.DBNullToStringConv(reader.GetValue(6));
                        DateTime dateCreated = reader.GetDateTime(7);
                        short userId = Convert.ToInt16(reader.GetInt64(8));

                        ProjectDto project = new ProjectDto((int)id, dateCreated, title, comments, archive, labo, auction, disc, userId);
                        project.AcceptChanged();
                        list.Add(project);
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

        public override ProjectDto Save(ProjectDto data)
        {
            throw new NotImplementedException();
        }

        public override ProjectDto Update(ProjectDto data)
        {
            throw new NotImplementedException();
        }
    }
}
