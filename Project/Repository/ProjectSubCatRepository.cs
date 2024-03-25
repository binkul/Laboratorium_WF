using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.SqlDataConstant;
using Laboratorium.ADO.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Project.Repository
{
    public class ProjectSubCatRepository : BasicCRUD<ProjectSubCategoryDto>
    {
        private static readonly SqlIndex SQL_INDEX = SqlIndex.ProjectSubCatIndex;
        private static readonly string TABLE_NAME = Table.PROJECT_SUB_TABLE;

        public ProjectSubCatRepository(SqlConnection connection) : base(connection, SQL_INDEX, TABLE_NAME)
        { }

        public override IList<ProjectSubCategoryDto> GetAll()
        {
            List<ProjectSubCategoryDto> list = new List<ProjectSubCategoryDto>();

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
                        long projectId = reader.GetInt64(1);
                        string name = reader.GetString(1);
                        DateTime dateCreated = reader.GetDateTime(7);
                        short userId = Convert.ToInt16(reader.GetInt64(8));

                        ProjectSubCategoryDto project = new ProjectSubCategoryDto(id, (int)projectId, dateCreated, name);
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

        public override ProjectSubCategoryDto Save(ProjectSubCategoryDto data)
        {
            throw new NotImplementedException();
        }

        public override ProjectSubCategoryDto Update(ProjectSubCategoryDto data)
        {
            throw new NotImplementedException();
        }
    }
}
