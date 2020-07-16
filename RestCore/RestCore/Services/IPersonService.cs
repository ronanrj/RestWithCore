using RestCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCore.Services.Implementations
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindId(int id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(int id);
    }
}
