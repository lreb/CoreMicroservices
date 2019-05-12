using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Library.Data.DataModels;
using Core.Library.Interfaces.Authentications;
using Core.Library.Messages;
using Core.Library.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Core.Library.Business.Authentications
{
    public class Authentications : IAuthentications
    {
        public Authentications()
        {
        }

        public StandardResult Authenticate(Users user, AppSettings appSettings)
        {
            try
            {


                // validate in db first. next version
                //_users.SingleOrDefault(x => x.Username == username && x.Password == password);

                // return null if user not found
                if (user == null)
                    return StandardResult.WarningResult("authentication.warning");

                user.FirstName = "Luis";
                user.LastName = "Espinoza";

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.FirstName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                // remove password before returning
                user.Password = null;

                // result.Data = user;
                return StandardResult.SuccessResult("authentication.success", user);
            }
            catch (Exception ex)
            {
                return StandardResult.FailResult("authentication.fails", ex);
            }

        }
    }
}
