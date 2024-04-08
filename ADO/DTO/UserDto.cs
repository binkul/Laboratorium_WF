using System;
using System.Collections.Generic;

namespace Laboratorium.ADO.DTO
{
    public class UserDto
    {
        private short _id;
        private string _name;
        private string _surname;
        private string _email;
        private string _login;
        private string _password;
        private string _permission;
        private string _identifier;
        private bool _active;
        private DateTime _dateCreated;
        private RowState _rowState = RowState.ADDED;
        private CrudState _crudState = CrudState.OK;

        public UserDto() { }

        public UserDto(short id, string name, string surname, string email, string login, string password, string permission, 
            string identifier, bool active, DateTime dateCreated)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _email = email;
            _login = login;
            _password = password;
            _permission = permission;
            _identifier = identifier;
            _active = active;
            _dateCreated = dateCreated;
        }

        public UserDto(short id, string name, string login, string identifier, bool active)
        {
            _id = id;
            _name = name;
            _login = login;
            _identifier = identifier;
            _active = active;
            _rowState = RowState.UNCHANGED;
        }

        public short Id
        {
            get => _id;
            set
            {
                _id = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Permission
        {
            get => _permission;
            set
            {
                _permission = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public string Identifier
        {
            get => _identifier;
            set
            {
                _identifier = value;
                _rowState = RowState.MODIFIED;
            }
        }

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
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

        public RowState GetRowState => _rowState;

        public CrudState GetCrudState
        {
            get => _crudState;
            set => _crudState = value;
        }

        public void AcceptChanged()
        {
            _rowState = RowState.UNCHANGED;
        }

    }
}
