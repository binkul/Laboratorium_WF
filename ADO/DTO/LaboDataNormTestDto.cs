﻿using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDataNormTestDto
    {
        private short _position;
        private string _norm;
        private string _description;
        private string _requirement;
        private string _result;
        private string _substrate;
        private string _comment;
        private byte _groupId;
        private RowState _rowState = RowState.ADDED;
        private readonly IService _service;
        private DateTime _dateCreated = DateTime.Today;
        private DateTime _dateUpdated;

        public int Id { get; set; } = 0;
        public int TmpId { get; set; } = 0;
        public int LaboId { get; set; }
        public int Days { get; private set; } = 0;
        public CrudState CrudState { get; set; } = CrudState.OK;


        public LaboDataNormTestDto(int days, int id, int laboId, short position, string norm, string description, string requirement, 
            string result, string substrate, string comment, byte groupId, DateTime dateCreated, DateTime dateUpdated, IService service)
        {
            Days = days;
            Id = id;
            TmpId = id;
            LaboId = laboId;
            _dateCreated = dateCreated;
            _position = position;
            _norm = norm;
            _description = description;
            _requirement = requirement;
            _result = result;
            _substrate = substrate;
            _comment = comment;
            _groupId = groupId;
            _dateUpdated = dateUpdated;
            _service = service;
        }

        public LaboDataNormTestDto(int tmpId, int laboId, short position, string norm, string description, string requirement, string result, 
            string substrate, string comment, byte groupId, DateTime dateUpdated, IService service)
        {
            TmpId = tmpId;
            LaboId = laboId;
            _position = position;
            _norm = norm;
            _description = description;
            _requirement = requirement;
            _result = result;
            _substrate = substrate;
            _comment = comment;
            _groupId = groupId;
            _service = service;
            _dateUpdated = dateUpdated;
        }

        public LaboDataNormTestDto(int tmpId, int laboId, short position, string norm, string description, string substrate, byte groupId, IService service)
        {
            TmpId = tmpId;
            LaboId = laboId;
            _position = position;
            _norm = norm;
            _description = description;
            _substrate = substrate;
            _groupId = groupId;
            _service = service;
            _dateUpdated = DateTime.Today;
        }

        private void ChangeState(RowState state)
        {
            _dateUpdated = DateTime.Today;
            UpdaetRowState(state);
        }

        private void UpdateDays()
        {
            Days = (int)(DateUpdated - DateCreated).TotalDays;
        }

        private void UpdaetRowState(RowState state)
        {
            UpdateDays();
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            if (_service != null)
                _service.Modify(_rowState);
        }

        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                _dateCreated = value;
                UpdaetRowState(RowState.MODIFIED);
            }
        }

        public DateTime DateUpdated
        {
            get => _dateUpdated;
            set
            {
                _dateUpdated = value;
                UpdaetRowState(RowState.MODIFIED);
            }
        }

        public short Position
        {
            get => _position;
            set
            {
                _position = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Norm
        {
            get => _norm;
            set
            {
                _norm = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Requirement
        {
            get => _requirement;
            set
            {
                _requirement = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Substrate
        {
            get => _substrate;
            set
            {
                _substrate = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Comments
        {
            get => _comment;
            set
            {
                _comment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte GroupId
        {
            get => _groupId;
            set
            {
                _groupId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public RowState GetRowState => _rowState;

        public void AcceptChanges()
        {
            _rowState = RowState.UNCHANGED;
        }

    }
}
