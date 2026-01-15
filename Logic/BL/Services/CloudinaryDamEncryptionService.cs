using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CloudinaryDam.Logic.BL.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }

    public class CloudinaryDamEncryptionService : IEncryptionService
    {
        private readonly string _masterKey;
        public CloudinaryDamEncryptionService(IConfiguration config)
        {
            _masterKey = config["CloudinaryDam:EncryptionKey"];
            if (string.IsNullOrEmpty(_masterKey))
            {
                throw new InvalidOperationException("Encryption key is not configured. Please set 'CloudinaryDam:EncryptionKey' in the configuration.");
            }
        }
        public string Encrypt(string plainText)
        {
            using (var pdb = new Rfc2898DeriveBytes(
                password: _masterKey,
                salt: Encoding.UTF8.GetBytes("static-salt"),
                iterations: 1000,
                hashAlgorithm: HashAlgorithmName.SHA256))
            using (var aes = Aes.Create())
            {
                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using var encryptor = aes.CreateEncryptor();
                var bytes = Encoding.UTF8.GetBytes(plainText);
                var cipher = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);

                return Convert.ToBase64String(cipher);
            }
        }

        public string Decrypt(string cipherText)
        {
            using (var pdb = new Rfc2898DeriveBytes(
                password: _masterKey,
                salt: Encoding.UTF8.GetBytes("static-salt"),
                iterations: 1000,
                hashAlgorithm: HashAlgorithmName.SHA256))
            using (var aes = Aes.Create())
            {
                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using var decryptor = aes.CreateDecryptor();
                var cipher = Convert.FromBase64String(cipherText);
                try
                {
                    var plain = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
                    return Encoding.UTF8.GetString(plain);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
