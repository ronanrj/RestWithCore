using RestCore.Data.Converters;
using RestCore.Data.VO;
using RestCore.Model;
using RestCore.Repository.Generic;
using System;
using System.Collections.Generic;

namespace RestCore.Business.Implementations
{
    public class BookBusinessImpl : IBookBusiness
    {

        private IRepository<Book> _repository;

        private readonly BookConverter _converter;

        public BookBusinessImpl(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }


        public BookVO Create(BookVO Book)
        {
            var bookEntity = _converter.Parse(Book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);                      

        }

        public bool Exist(int? id)
        {
            throw new NotImplementedException();
        }

        public List<BookVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public BookVO FindId(int id)
        {
            return _converter.Parse(_repository.FindId(id));
        }

        public BookVO Update(BookVO Book)
        {
            var bookEntity = _converter.Parse(Book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

       
    }
}
