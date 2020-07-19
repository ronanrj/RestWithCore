﻿using RestCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCore.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindId(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exist(long? id);
    }
}
