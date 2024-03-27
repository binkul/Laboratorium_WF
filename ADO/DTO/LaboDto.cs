using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; private set; }
        private string _title;
        private int _projectId;
        private string _goal;
        private double? _density;
        private string _conclusion;
        private string _observation;
        private bool _isDeleted = false;
        private short _userId;
        private UserDto _user;
        private ProjectDto _project;
        private RowState _rowState = RowState.ADDED;

        public LaboDto() { }

        public LaboDto(int id, string title, DateTime dateCreated, DateTime dateUpdated, int projectId, string goal, double? density, 
            string conclusion, string observation, bool isDeleted, short userId)
        {
            Id = id;
            DateCreated = dateCreated;
            _title = title;
            DateUpdated = dateUpdated;
            _projectId = projectId;
            _goal = goal;
            _density = density;
            _conclusion = conclusion;
            _observation = observation;
            _isDeleted = isDeleted;
            _userId = userId;
        }

        public LaboDto(int id, string title, DateTime dateCreated, int projectId, short userId)
        {
            Id = id;
            DateCreated = dateCreated;
            _title = title;
            DateUpdated = dateCreated;
            _projectId = projectId;
            _userId = userId;
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public int ProjectId
        {
            get => _projectId;
            set
            {
                _projectId = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string Goal
        {
            get => _goal;
            set
            {
                _goal = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public double? Density
        {
            get => _density;
            set
            {
                _density = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string Conclusion
        {
            get => _conclusion;
            set
            {
                _conclusion = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string Observation
        {
            get => _observation;
            set
            {
                _observation = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public short UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public UserDto User
        {
            get => _user;
            set => _user = value;
        }

        public string UserShortcut => _user != null ? _user.Identifier : "ND";

        public ProjectDto Project
        {
            get => _project;
            set => _project = value;
        }

        public string ProjectName => _project != null ? _project.Title : "-- Brak --";

        public RowState GetRowState => _rowState;

        public void AcceptChanged()
        {
            _rowState = RowState.UNCHANGED;
        }
    }
}
