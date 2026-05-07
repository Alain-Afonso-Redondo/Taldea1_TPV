using System;
using System.Security.Cryptography;
using System.Text;

namespace Taldea1TPV
{
    internal static class TxatCryptoLaguntzailea
    {
        private const string Prefix = "[AES]|";
        private const string Secret = "1Taldea-Txat-AES-2026";
        private const int IvSize = 16;

        public static string Enkriptatu(string testua)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = SortuGakoa();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(testua);
                    byte[] encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    byte[] payload = new byte[aes.IV.Length + encrypted.Length];

                    Buffer.BlockCopy(aes.IV, 0, payload, 0, aes.IV.Length);
                    Buffer.BlockCopy(encrypted, 0, payload, aes.IV.Length, encrypted.Length);

                    return Convert.ToBase64String(payload);
                }
            }
        }

        public static string DesenkriptatuBeharBada(string testua)
        {
            if (string.IsNullOrWhiteSpace(testua))
                return testua;

            try
            {
                string encryptedText = testua.StartsWith(Prefix)
                    ? testua.Substring(Prefix.Length)
                    : testua;

                byte[] payload = Convert.FromBase64String(encryptedText);
                if (payload.Length <= IvSize)
                    return testua;

                byte[] iv = new byte[IvSize];
                byte[] encrypted = new byte[payload.Length - IvSize];
                Buffer.BlockCopy(payload, 0, iv, 0, IvSize);
                Buffer.BlockCopy(payload, IvSize, encrypted, 0, encrypted.Length);

                using (var aes = Aes.Create())
                {
                    aes.Key = SortuGakoa();
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        byte[] plainBytes = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
                        return Encoding.UTF8.GetString(plainBytes);
                    }
                }
            }
            catch
            {
                return testua;
            }
        }

        private static byte[] SortuGakoa()
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(Secret));
            }
        }
    }
}
