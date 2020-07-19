using Microsoft.EntityFrameworkCore;
using RestCore.Model.Base;
using RestCore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestCore.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        private SqlContext _contex;
        private DbSet<T> dataset;

        public GenericRepository(SqlContext contex)
        {
            _contex = contex;
            dataset = contex.Set<T>();
        }



        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _contex.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            return item;
        }

        public void Delete(long Id)
        {
            var result = dataset.SingleOrDefault(i => i.Id.Equals(Id));

            try
            {
                if (result != null)
                {
                    dataset.Remove(result);
                    _contex.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool Exist(long? Id)
        {
            return dataset.Any(p => p.Id == Id);
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindId(long Id)
        {
            return dataset.SingleOrDefault(p=>p.Id == Id);

        }

        public T Update(T item)
        {
            if (!Exist(item.Id)) return null;

            var result = dataset.SingleOrDefault(p => p.Id == item.Id);

            try
            {
                _contex.Entry(result).CurrentValues.SetValues(item);
                _contex.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            return item;
        }
    }
}
