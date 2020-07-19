using RestCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCore.Repository
{
    public interface ILoginRepository
    {
        User FindByLogin(string user);

    }
}
