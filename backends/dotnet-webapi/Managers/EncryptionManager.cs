
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace dotnet_webapi
{
    public class EncryptionManager
    {
        public static string EncryptString(string objText, string objKeycode)
        {
            byte[] objInitVectorBytes = Encoding.ASCII.GetBytes("fdc3726f798f4860");
            byte[] objPlainTextBytes = Encoding.UTF8.GetBytes(objText);
            PasswordDeriveBytes objPassword = new PasswordDeriveBytes(objKeycode, null);
            byte[] objKeyBytes = objPassword.GetBytes(256 / 8);
            RijndaelManaged objSymmetricKey = new RijndaelManaged();
            objSymmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform objEncryptor = objSymmetricKey.CreateEncryptor(objKeyBytes, objInitVectorBytes);
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objEncryptor, CryptoStreamMode.Write);
            objCryptoStream.Write(objPlainTextBytes, 0, objPlainTextBytes.Length);
            objCryptoStream.FlushFinalBlock();
            byte[] objEncrypted = objMemoryStream.ToArray();
            objMemoryStream.Close();
            objCryptoStream.Close();
            return Convert.ToBase64String(objEncrypted);
        }

        public static string DecryptString(string EncryptedText, string Key)
        {
            byte[] objInitVectorBytes = Encoding.ASCII.GetBytes("fdc3726f798f4860");
            byte[] objDeEncryptedText = Convert.FromBase64String(EncryptedText); 
            PasswordDeriveBytes objPassword = new PasswordDeriveBytes(Key, null);
            byte[] objKeyBytes = objPassword.GetBytes(256 / 8);
            RijndaelManaged objSymmetricKey = new RijndaelManaged();
            objSymmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform objDecryptor = objSymmetricKey.CreateDecryptor(objKeyBytes, objInitVectorBytes);
            MemoryStream objMemoryStream = new MemoryStream(objDeEncryptedText);
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDecryptor, CryptoStreamMode.Read);
            byte[] objPlainTextBytes = new byte[objDeEncryptedText.Length];
            int objDecryptedByteCount = objCryptoStream.Read(objPlainTextBytes, 0, objPlainTextBytes.Length);
            objMemoryStream.Close();
            objCryptoStream.Close();
            return Encoding.UTF8.GetString(objPlainTextBytes, 0, objDecryptedByteCount);
        }

        public static string EncryptString1(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string DecryptString1(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }		
    }
}


