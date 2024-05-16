using System.Security.Cryptography;
using System.Text;

namespace ReportAndSolveAPI.Security
{
    public class Sha512HashGenerator : IHashGenerator
    {
        public string IncryptData(string data)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(data);

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                string hashResult = BitConverter.ToString(hashBytes).Replace("-", "");

                return hashResult;
            }
        }

        public bool VerifyHash(string data, string hash)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(data);

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                string hashResult = BitConverter.ToString(hashBytes).Replace("-", "");

                return hash.Equals(hashResult);
            }
        }
    }
}
