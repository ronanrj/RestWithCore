using RestCore.Data.VO;
using RestCore.Repository;
using RestCore.Security.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace RestCore.Business.Implementations
{
    public class LoginBusinessImpl : ILoginBusiness
    {

        private ILoginRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfiguration _tokenConfiguration;



        public LoginBusinessImpl(ILoginRepository repository, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
        }


        public object FindByLogin(UserVO user)
        {
            bool credentialsIsValid = false;
            if (user != null & !string.IsNullOrWhiteSpace(user.login))
            {
                var baseUser = _repository.FindByLogin(user.login);
                credentialsIsValid = (baseUser != null && user.login == baseUser.login && user.accessKey == baseUser.accessKey);

            }
            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.login,"Login"),
                    new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName,user.login)
                        }

                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);
                return SuccessObject(createDate, expirationDate, token);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor{ 
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });
            var token = handler.WriteToken(securityToken);

            return token;

        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to authenticate"
            };
        }

        private object SuccessObject(DateTime createDate , DateTime expirationDate , string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
