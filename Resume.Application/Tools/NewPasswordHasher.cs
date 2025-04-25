using System.Security.Cryptography;

namespace Resume.Application.Tools
{
    public static class NewPasswordHasher
    {
        public static string HashPassword(string password, string salt)
        {
            // Create a Rfc2898DeriveBytes object 
            // Adjust iterations count for desired security level (e.g., 10000+)
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(32));
            }
        }

        public static string GenerateSalt()
        {
            // Generate a cryptographically secure random salt
            byte[] randomBytes = new byte[24];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public static bool CompareHashes(string hash1, string hash2)
        {
            // Convert the hashes from Base64 strings to byte arrays
            byte[] hashBytes1 = Convert.FromBase64String(hash1);
            byte[] hashBytes2 = Convert.FromBase64String(hash2);

            // Compare the byte arrays directly for constant-time comparison
            // This prevents timing attacks that could reveal information about the password
            var len = hashBytes1.Length;
            var diff = 0;

            for (var i = 0; i < len; i++)
            {
                diff |= hashBytes1[i] ^ hashBytes2[i];
            }
            return diff == 0;
        }
    }
}
