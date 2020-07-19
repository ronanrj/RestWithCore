using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestCore.Business;
using RestCore.Data.VO;
using RestCore.Model;

namespace RestCore.Controllers
{

    [ApiVersion("1.0")]
    //[Route("api/[controller]/v{version:apiVersion}")] 
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        //declaração do serviço usado
        private ILoginBusiness _loginBusiness;
       

        //Injeção de uma instancia de IpersonService ao criar uma instancia de PersonController
        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [AllowAnonymous]        
        public object Post([FromBody] UserVO user)
        {            
            if (user == null) return BadRequest();
            return new ObjectResult(_loginBusiness.FindByLogin(user));
        }
               
    }
}
