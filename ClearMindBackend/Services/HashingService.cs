using System.Security.Cryptography;
using System.Text;
namespace Gemini_Competition.Services
{
    public class HashingService
    {
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string byteHash)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(byteHash))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static byte[] GetHashByte(string stringHash)
        {
            if (stringHash.Length % 2 != 0)
                throw new ArgumentException("Invalid length of the hash string.");

            byte[] bytes = new byte[stringHash.Length / 2];

            for (int i = 0; i < stringHash.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(stringHash.Substring(i, 2), 16);
            }

            return bytes;
        }

        public static bool CompareHash(string inputString, string hash)
        {
            byte[] hashedInputString = GetHash(inputString);
            byte[] bytesHash = GetHashByte(hash);
            if (hashedInputString.Length == bytesHash.Length)
            {
                int i = 0;
                while ((i < hashedInputString.Length) && (hashedInputString[i] == bytesHash[i]))
                {
                    i += 1;
                }
                if (i == hashedInputString.Length)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
