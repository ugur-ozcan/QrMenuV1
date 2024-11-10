// QRMenu.Infrastructure/Services/EncryptionService.cs
using QRMenu.Core.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace QRMenu.Infrastructure.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly string _key;

        public EncryptionService(string key = "your-secure-key-here")
        {
            _key = key;
        }

        public string Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            try
            {
                using var aes = Aes.Create();
                using var md5 = MD5.Create();
                aes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(_key));
                aes.IV = new byte[16];

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using var msEncrypt = new System.IO.MemoryStream();
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(text);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
            catch
            {
                return text;
            }
        }

        public string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText)) return encryptedText;

            try
            {
                using var aes = Aes.Create();
                using var md5 = MD5.Create();
                aes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(_key));
                aes.IV = new byte[16];

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(encryptedText));
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new System.IO.StreamReader(csDecrypt);
                return srDecrypt.ReadToEnd();
            }
            catch
            {
                return encryptedText;
            }
        }
    }
}