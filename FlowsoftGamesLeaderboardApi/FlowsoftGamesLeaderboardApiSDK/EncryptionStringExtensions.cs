using System;
using System.Security.Cryptography;
using System.Text;

namespace FlowsoftGamesLeaderboardApiSDK
{
    public static class EncryptionStringExtensions
    {
        internal static readonly byte[] _key = new byte[32]
        {
            16, 64, 93, 70, 5, 194, 164, 59,
            150, 160, 28, 145, 192, 211, 134, 139,
            176, 165, 121, 97, 162, 35, 214, 10,
            136, 225, 171, 49, 129, 153, 82, 237
        };

        public static byte[] AsIV(this string uidText)
        {
            return new Guid(uidText).ToByteArray();
        }

        public static string Crypt(this string text, byte[] iv)
        {
            var aesCSP = new AesCryptoServiceProvider
            {
                Key = _key,
                IV = iv
            };

            byte[] encQuote = EncryptString(aesCSP, text);

            return Convert.ToBase64String(encQuote);
        }

        public static string Decrypt(this string text, byte[] iv)
        {
            var aesCSP = new AesCryptoServiceProvider
            {
                Key = _key,
                IV = iv
            };

            return DecryptBytes(aesCSP, Convert.FromBase64String(text));
        }

        internal static byte[] EncryptString(SymmetricAlgorithm symAlg, string inString)
        {
            byte[] inBlock = Encoding.Unicode.GetBytes(inString);
            ICryptoTransform xfrm = symAlg.CreateEncryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);

            return outBlock;
        }

        internal static string DecryptBytes(SymmetricAlgorithm symAlg, byte[] inBytes)
        {
            ICryptoTransform xfrm = symAlg.CreateDecryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(inBytes, 0, inBytes.Length);

            return Encoding.Unicode.GetString(outBlock);
        }
    }
}