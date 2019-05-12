using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Library.Data.DataModels;
using Core.Library.Interfaces.Authentications;
using Core.Library.Messages;
using Core.Library.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Auth.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationsController : Controller
    {
        private IAuthentications _authentications;
        private readonly AppSettings _appSettings;

        public AuthenticationsController(IOptions<AppSettings> appSettings,
            IAuthentications authentications)
        {
            _appSettings = appSettings.Value;
            _authentications = authentications;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<StandardResult> Authenticate([FromBody]Users userParam)
        {
            //var user = _userService.Authenticate(userParam.Username, userParam.Password);
            var userObject = new Users { Username = "user", Password = "pass" }; // Authenticate("user", "pass");
            return _authentications.Authenticate(userObject, _appSettings);
            // var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var user = _authentications.Authenticate(userObject, _appSettings);
            //if (!user.Success == null)
            //  return BadRequest(new { message = "Username or password is incorrect" });
            //return StandardResult
            //return Ok(user);
        }
    }
}
