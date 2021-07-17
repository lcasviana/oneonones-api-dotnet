using System.Text;

namespace Oneonones.Domain.Extensions
{
    public static class HashExtension
    {
        public static string Digest(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = System.Security.Cryptography.SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString().ToLower();
        }
    }
}