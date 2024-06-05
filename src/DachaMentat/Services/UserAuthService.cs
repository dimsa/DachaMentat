using DachaMentat.Config;
using DachaMentat.Db;
using DachaMentat.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DachaMentat.Services
{
    public class UserAuthService : IUserAuthService
    {
        private IDataSourceService _dataSource;

        public UserAuthService(IDataSourceService dataSource)
        {
            _dataSource = dataSource;
        }

        public string CreateToken(string login, string password)
        {
            using (var db = _dataSource.GetUsersDbContext())
            {
                if (db.Users.Count() == 0)
                {
                    var setup = Setup(login, password);
                    if (!setup)
                    {
                        throw new MentatDbException("Setup was not succesful");
                    }
                }

                var hash = HashProvider.GetHash(password);

                var user = db.Users.FirstOrDefault(user => user.Login == login.ToLower() && user.PasswordHash == hash);

                if (user != null)
                {


                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, login),
                        new Claim(ClaimTypes.Role, "Administrators")
                    };

                    var key = AuthOptions.GetSymmetricSecurityKey();
                    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                                           issuer: AuthOptions.ISSUER,
                                           audience: AuthOptions.AUDIENCE,
                                           claims: claims,
                                           expires: DateTime.UtcNow.AddDays(1),
                                           signingCredentials: cred
                    );
                    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                    return jwt;
                }

                throw new MentatDbException("The User was not find or password is incorrect");
            }
        }

        public bool Setup(string login, string password)
        {
            using (var db = _dataSource.GetUsersDbContext())
            {
                var hash = HashProvider.GetHash(password);

                if (db.Users.Count() != 0)
                {
                    throw new MentatDbException("The Mentat is already installed. Please contact administrator.");
                }

                var user = new User()
                {
                    Login = login.ToLower(),
                    PasswordHash = hash,
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }
    }
}
