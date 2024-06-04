using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DachaMentat.Config
{
    public class HashProvider
    {
        private static byte[] _salt = new byte[] { };

        public static string  GetHash(string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
