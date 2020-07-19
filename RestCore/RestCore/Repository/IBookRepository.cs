using RestCore.Model;
using System.Collections.Generic;

namespace RestCore.Repository
{
    public interface IBookRepository
    {
        Book Create(Book Book);
        Book FindId(int id);
        List<Book> FindAll();
        Book Update(Book Book);
        void Delete(int id);
        //bool Exist(int? id);

    }
}
