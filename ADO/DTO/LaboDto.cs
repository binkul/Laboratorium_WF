using Laboratorium.ADO.Service;
using System;
using static System.Windows.Forms.AxHost;

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
        private IService _service;

        public LaboDto() { }

        public LaboDto(int id, string title, DateTime dateCreated, DateTime dateUpdated, int projectId, string goal, double? density, 
            string conclusion, string observation, bool isDeleted, short userId, IService service)
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
            _service = service;
        }

        public LaboDto(int id, string title, DateTime dateCreated, int projectId, short userId, IService service)
        {
            Id = id;
            DateCreated = dateCreated;
            _title = title;
            DateUpdated = dateCreated;
            _projectId = projectId;
            _userId = userId;
            _service = service;
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            DateUpdated = DateTime.Today;
            if (_service != null)
                _service.Modify(state);
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public int ProjectId
        {
            get => _projectId;
            set
            {
                _projectId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Goal
        {
            get => _goal;
            set
            {
                _goal = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Density
        {
            get => _density;
            set
            {
                _density = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Conclusion
        {
            get => _conclusion;
            set
            {
                _conclusion = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Observation
        {
            get => _observation;
            set
            {
                _observation = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public short UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                ChangeState(RowState.MODIFIED);
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
            if (_service != null)
                _service.Modify(RowState.UNCHANGED);
        }
    }
}
