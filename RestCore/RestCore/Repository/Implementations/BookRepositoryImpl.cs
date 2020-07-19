using RestCore.Model;
using RestCore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestCore.Repository.Implementations
{
    public class BookRepositoryImpl : IBookRepository
    {

        //    private SqlContext _contex;

        //    public BookRepositoryImpl(SqlContext contex)
        //    {
        //        _contex = contex;
        //    }


        //    public Book Create(Book Book)
        //    {
        //        try
        //        {
        //            _contex.Add(Book);
        //            _contex.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {

        //            throw;
        //        }
        //        return Book;
        //    }

        //    public void Delete(int id)
        //    {
        //        var result = _contex.Books.SingleOrDefault(p => p.Id.Equals(id));

        //        try
        //        {
        //            if (result != null)
        //            {
        //                _contex.Books.Remove(result);
        //                _contex.SaveChanges();
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            throw;
        //        }


        //    }

        //    public List<Book> FindAll()
        //    {
        //        return _contex.Books.ToList();
        //    }

        //    public Book FindId(int id)
        //    {
        //        return _contex.Books.SingleOrDefault(p => p.Id.Equals(id));
        //    }

        //    public Book Update(Book Book)
        //    {
        //        if (!Exist(Book.Id)) return null;

        //        var result = _contex.Books.SingleOrDefault(p => p.Id.Equals(Book.Id));

        //        try
        //        {
        //            _contex.Entry(result).CurrentValues.SetValues(Book);
        //            _contex.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {

        //            throw;
        //        }
        //        return Book;
        //    }

        //    private bool Exist(int? id)
        //    {
        //        return _contex.Books.Any(p => p.Id.Equals(id));
        //    }
        //}
        public Book Create(Book Book)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> FindAll()
        {
            throw new NotImplementedException();
        }

        public Book FindId(int id)
        {
            throw new NotImplementedException();
        }

        public Book Update(Book Book)
        {
            throw new NotImplementedException();
        }
    }
}
