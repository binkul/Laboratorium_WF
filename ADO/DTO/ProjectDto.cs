using System;

namespace Laboratorium.ADO.DTO
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        private string _title;
        private string _comments;
        private bool _isArchive;
        private bool _isLabo;
        private bool _isAuction;
        private string _localDisk;
        private short _userId;
        private UserDto _user;
        private RowState _rowState = RowState.ADDED;

        public ProjectDto(int id, DateTime dateCreated, string title, string comments, bool isArchive, bool isLabo, bool isAuction, string localDisk, short userId)
        {
            Id = id;
            DateCreated = dateCreated;
            _title = title;
            _comments = comments;
            _isArchive = isArchive;
            _isLabo = isLabo;
            _isAuction = isAuction;
            _localDisk = localDisk;
            _userId = userId;
        }

        public ProjectDto(string title, bool isArchive, bool isLabo, bool isAuction, UserDto user)
        {
            _title = title;
            _isArchive = isArchive;
            _isLabo = isLabo;
            _isAuction = isAuction;
            _user = user;
            DateCreated = DateTime.Today;
            user.Id = user.Id;
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
            }
        }

        public string Comments
        {
            get => _comments;
            set
            {
                _comments = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
            }
        }

        public bool IsArchive
        {
            get => _isArchive;
            set
            {
                _isArchive = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
            }
        }

        public bool IsLabo
        {
            get => _isLabo;
            set
            {
                _isLabo = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
            }
        }

        public bool IsAuction
        {
            get => _isAuction;
            set
            {
                _isAuction = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
            }
        }

        public string LocalDisk
        {
            get => _localDisk;
            set
            {
                _localDisk = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
            }
        }

        public short UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
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
