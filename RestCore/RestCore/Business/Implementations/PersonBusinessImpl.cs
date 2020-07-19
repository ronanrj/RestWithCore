using RestCore.Data.Converters;
using RestCore.Data.VO;
using RestCore.Model;
using RestCore.Repository.Generic;
using System;
using System.Collections.Generic;

namespace RestCore.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {

        private IRepository<Person> _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImpl(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }


        public PersonVO Create(PersonVO Person)
        {
            var personEntity = _converter.Parse(Person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);                      

        }

        public bool Exist(int? id)
        {
            return _repository.Exist(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public PersonVO FindId(int id)
        {
            return _converter.Parse(_repository.FindId(id));
        }

        public PersonVO Update(PersonVO Person)
        {
            var personEntity = _converter.Parse(Person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
            
        }

       
    }
}
