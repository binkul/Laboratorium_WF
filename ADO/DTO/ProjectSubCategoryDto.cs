using System;

namespace Laboratorium.ADO.DTO
{
    public class ProjectSubCategoryDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateCreated { get; set; }
        private string _name;
        private RowState _rowState = RowState.ADDED;

        public ProjectSubCategoryDto(int id, int projectId, DateTime dateCreated, string name)
        {
            Id = id;
            ProjectId = projectId;
            DateCreated = dateCreated;
            _name = name;
        }

        public ProjectSubCategoryDto(int projectId, string name)
        {
            ProjectId = projectId;
            _name = name;
            DateCreated = DateTime.Today;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
            }
        }

        public RowState GetRowState => _rowState;

        public void AcceptChanged()
        {
            _rowState = RowState.UNCHANGED;
        }

    }
}
