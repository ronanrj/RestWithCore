using RestCore.Model;
using RestCore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestCore.Repository.Implementations
{
    public class LoginRepositoryImpl : ILoginRepository
    {

        private SqlContext _contex;

        public LoginRepositoryImpl(SqlContext contex)
        {
            _contex = contex;
        }


        public User FindByLogin(string user)
        {
            return _contex.Users.SingleOrDefault(u => u.login == user);
        }

    }
}
