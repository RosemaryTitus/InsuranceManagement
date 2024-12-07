using InsuranceManagement.Models;
using InsuranceManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagement : ControllerBase
    {
        public readonly IConfiguration _Configuration;
        public readonly InsuranceManagementDatabaseContext _databaseContext;
        public readonly userAction _userAction;

        public UserManagement(IConfiguration configuration, InsuranceManagementDatabaseContext databaseContext,userAction userAction) 
        {
            _Configuration = configuration;
            _databaseContext = databaseContext;
            _userAction = userAction;
        }
        //public string HashPassword(string password)
        //{
        //    string pass = BCrypt.Net.BCrypt.HashPassword(password);
        //    return pass;
        //}
        [HttpPut]
        [Route("Registration")]
        public async Task<IActionResult> Registration(User user)
        {
            //Hash password

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var result = await _userAction.userRegistration(user);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string username, string password)
        {
           // string pass = BCrypt.Net.BCrypt.HashPassword(password);
            string result = _userAction.userLogin(username, password);
            return Ok(result);
        }
    }
}
