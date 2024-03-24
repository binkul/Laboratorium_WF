using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; private set; }
        private string _title;
        private int _project;
        private string _goal;
        private double? _density;
        private string _conclusion;
        private string _observation;
        private bool _isDeleted = false;
        private short _userId;
        private UserDto _user;
        private RowState _rowState = RowState.ADDED;

        public LaboDto() { }

        public LaboDto(int id, string title, DateTime dateCreated, DateTime dateUpdated, int project, string goal, double? density, 
            string conclusion, string observation, bool isDeleted, short userId)
        {
            Id = id;
            DateCreated = dateCreated;
            _title = title;
            DateUpdated = dateUpdated;
            _project = project;
            _goal = goal;
            _density = density;
            _conclusion = conclusion;
            _observation = observation;
            _isDeleted = isDeleted;
            _userId = userId;
        }

        public LaboDto(int id, string title, DateTime dateCreated, int project, short userId)
        {
            Id = id;
            DateCreated = dateCreated;
            _title = title;
            DateUpdated = dateCreated;
            _project = project;
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

        public int Project
        {
            get => _project;
            set
            {
                _project = value;
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

        public RowState GetRowState => _rowState;

        public void AcceptChanged()
        {
            _rowState = RowState.UNCHANGED;
        }
    }
}
