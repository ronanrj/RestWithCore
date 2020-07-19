using RestCore.Data.VO;
using RestCore.Model;
using System.Collections.Generic;

namespace RestCore.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO Book);
        BookVO FindId(int id);
        List<BookVO> FindAll();
        BookVO Update(BookVO Book);
        void Delete(int id);
        bool Exist(int? id);

    }
}
