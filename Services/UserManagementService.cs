using InsuranceManagement.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InsuranceManagement.Services
{
    public interface userAction
    {
        public Task<String> userRegistration(User user);
        public String userLogin(string username, string password);

    }
    public class UserManagementService:userAction
    {
        public readonly InsuranceManagementDatabaseContext DatabaseContext;
        public readonly IConfiguration Configuration;
        public UserManagementService(InsuranceManagementDatabaseContext databaseContext, IConfiguration configuration) 
        {
            DatabaseContext = databaseContext;   
            Configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var Claims = new[]
            {
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Name!),
                new System.Security.Claims.Claim(ClaimTypes.Name, user.Name!),
                new System.Security.Claims.Claim(ClaimTypes.Role, user.Role)
            };
            var JwtSettings = Configuration.GetSection("Jwtsettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["Key"]!));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JwtSettings["Issuer"],
                audience: JwtSettings["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred
                );

            return  new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<String> userRegistration(User user)
        {
            string output;
            

            var result = await DatabaseContext.Users.FindAsync(user.Id);
            
            if (result == null)
            {
             
                await DatabaseContext.Users.AddAsync(user);
                DatabaseContext.SaveChanges();
                output = "User Added Successfully";
            }
            else
            {
                output = "User already Exist";
            }

            return output;
        }
        public string userLogin(string username, string password)
        {
            string output;
            var result = DatabaseContext.Users.FirstOrDefault(x => x.Email == username);
            if (result == null)
            {
                return "User doesn't exist";
            }
            else
            {
                bool hashpass = BCrypt.Net.BCrypt.Verify(password, result.Password);
                if(hashpass)
                {
                    output = GenerateToken(result);
                    return output;
                }
                else
                {
                    return "Invalid credentials";
                }
                     
                
            }

        }



    }
    
}
