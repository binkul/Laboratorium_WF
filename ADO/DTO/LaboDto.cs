using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDto
    {
        private int _id;
        private string _title;
        private DateTime _dateCreated;
        private int _project;
        private string _goal;
        private float? _density;
        private string _conclusion;
        private string _observation;
        private RowState _rowState = RowState.ADDED;

        public LaboDto() { }

        public LaboDto(int id, string title, DateTime dateCreated, int project, string goal, float? density, string conclusion, string observation)
        {
            _id = id;
            _title = title;
            _dateCreated = dateCreated;
            _project = project;
            _goal = goal;
            _density = density;
            _conclusion = conclusion;
            _observation = observation;
        }

        public LaboDto(int id, string title, DateTime dateCreated, int project)
        {
            _id = id;
            _title = title;
            _dateCreated = dateCreated;
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

        public string Titled
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
                _rowState = RowState.MODIFIED;
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

        public float? Density
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

        public RowState GetRowState => _rowState;

        public void AcceptChanged()
        {
            _rowState = RowState.UNCHANGED;
        }
    }
}
