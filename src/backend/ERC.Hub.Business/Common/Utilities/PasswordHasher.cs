using System.Security.Cryptography;

namespace ERC.Hub.Business.Common.Utilities
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;

        public string Hash(string password)
        {
            var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{Iterations}.{salt}.{key}";
        }

        public bool Validate(string hashed, string password)
        {
            if (hashed == null)
                throw new ArgumentException("Hash is null");

            var hashPart = hashed.Split('.', 3);
            if (hashPart.Length != 3)
                throw new FormatException("Unexpected hash format");

            var iterations = Convert.ToInt32(hashPart[0]);
            var salt = Convert.FromBase64String(hashPart[1]);
            var key = Convert.FromBase64String(hashPart[2]);

            var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(KeySize);
            var verified = keyToCheck.SequenceEqual(key);

            return verified;
        }
    }
}
