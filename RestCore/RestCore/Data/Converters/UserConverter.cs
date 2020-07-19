using RestCore.Data.Converter;
using RestCore.Data.VO;
using RestCore.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestCore.Data.Converters
{

    public class UserConverter : IParser<UserVO, User>, IParser<User, UserVO>
    {
        public User Parse(UserVO origin)
        {
            if (origin == null) return new User();
            return new User
            {
                login = origin.login,
                accessKey = origin.accessKey
            };
        }

        public UserVO Parse(User origin)
        {
            if (origin == null) return new UserVO();
            return new UserVO
            {
                login = origin.login,
                accessKey = origin.accessKey
            };
        }

        public List<User> ParseList(List<UserVO> origin)
        {
            if (origin == null) return new List<User>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UserVO> ParseList(List<User> origin)
        {
            if (origin == null) return new List<UserVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
