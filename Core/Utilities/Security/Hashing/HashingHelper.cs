using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreateHash(string textWillBeHash, out byte[] textHash, out byte[] textSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                textSalt = hmac.Key;
                textHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(textWillBeHash));
            }
        }

        public static bool VerifyHash(string textWillBeHash, byte[] textHash, byte[] textSalt)
        {
            using (var hmac = new HMACSHA512(textSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(textWillBeHash));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != textHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
