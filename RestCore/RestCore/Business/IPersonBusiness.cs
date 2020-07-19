using RestCore.Data.VO;
using System.Collections.Generic;

namespace RestCore.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO PersonVO);
        PersonVO FindId(int id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO PersonVO);
        void Delete(int id);
        bool Exist(int? id);

    }
}
