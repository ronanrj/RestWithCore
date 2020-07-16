using RestCore.Model;
using RestCore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestCore.Services.Implementations
{
    public class PersonServicesImpl : IPersonService
    {

        private SqlContext _contex;

        public PersonServicesImpl(SqlContext contex)
        {
            _contex = contex;
        }


        public Person Create(Person person)
        {
            try
            {
                _contex.Add(person);
                _contex.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            return person;
        }

        public void Delete(int id)
        {
            var result = _contex.Persons.SingleOrDefault(p => p.Id.Equals(id));

            try
            {
                if (result != null)
                {
                    _contex.Persons.Remove(result);
                    _contex.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            

        }

        public List<Person> FindAll()
        {
            return _contex.Persons.ToList();
        }

        public Person FindId(int id)
        {
            return _contex.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exist(person.Id)) return new Person();

            var result = _contex.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            try
            {
                _contex.Entry(result).CurrentValues.SetValues(person);
                _contex.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            return person;
        }

        private bool Exist(int? id)
        {
            return _contex.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
