using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MyShape
{
    public static class PluginValidator
    {
        // Method to calculate SHA-256 hash of a file
        public static string GetFileHash(string filePath)
        {
            if (!File.Exists(filePath))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var hashBytes = sha256.ComputeHash(stream);
                    // Convert bytes to hex string
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}