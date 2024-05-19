﻿namespace Laboratorium.ADO.Service
{
    public interface IDgvService
    {
        void PrepareData();
        bool Save();
        bool Delete();
        void AddNew(int number);
        void SynchronizeData(int LaboId);
    }
}