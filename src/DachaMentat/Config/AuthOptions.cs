using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DachaMentat.Config
{
    public class AuthOptions
    {
        // Token publisher
        public const string ISSUER = "DachaMentat"; 

        // Token consumer
        public const string AUDIENCE = "DachaMentang";

#if DEBUG
        private const string KEY = "1234567890____PLEASE_REPLACE_THIS_SECRETKEY____0987654321";
#else
        private const string KEY = "";
#endif

        public const int LIFETIME = 1; // время жизни токена - 1 минута

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(KEY));
            //return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
