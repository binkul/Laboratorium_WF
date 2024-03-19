using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDto
    {
        private int _id;
        private string _title;
        private DateTime _dateCreated;
        private DateTime _dateUpdated;
        private int _project;
        private string _goal;
        private double? _density;
        private string _conclusion;
        private string _observation;
        private bool _isDeleted = false;
        private RowState _rowState = RowState.ADDED;

        public LaboDto() { }

        public LaboDto(int id, string title, DateTime dateCreated, DateTime dateUpdated, int project, string goal, double? density, string conclusion, string observation, bool isDeleted)
        {
            _id = id;
            _title = title;
            _dateCreated = dateCreated;
            _dateUpdated = dateUpdated;
            _project = project;
            _goal = goal;
            _density = density;
            _conclusion = conclusion;
            _observation = observation;
            _isDeleted = isDeleted;
        }

        public LaboDto(int id, string title, DateTime dateCreated, int project)
        {
            _id = id;
            _title = title;
            _dateCreated = dateCreated;
            _dateUpdated = dateCreated;
            _project = project;
        }

        public int Id
        {
            get => _id;
            set 
            {
                _id = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                _dateCreated = value;
            }
        }

        public DateTime DateUpdated
        {
            get => _dateUpdated;
            set
            {
                _dateUpdated = value;
            }
        }

        public int Project
        {
            get => _project;
            set
            {
                _project = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Goal
        {
            get => _goal;
            set
            {
                _goal = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public double? Density
        {
            get => _density;
            set
            {
                _density = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Conclusion
        {
            get => _conclusion;
            set
            {
                _conclusion = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Observation
        {
            get => _observation;
            set
            {
                _observation = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public RowState GetRowState => _rowState;

        public void AcceptChanged()
        {
            _rowState = RowState.UNCHANGED;
        }
    }
}
