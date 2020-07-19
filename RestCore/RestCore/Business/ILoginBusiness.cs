using RestCore.Data.VO;

namespace RestCore.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(UserVO uservo);

    }
}
