using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

/*************************************************************
 * CLR Version:  4.0.30319.42000
 * NameSpace:    Library
 * FileName:     Class1
 * Create By:    a7498
 * Email:        kevin@o3e.net
 * Create Time:  2019/10/19 18:49:02
 * Description:
 *
 *============================================================
 * Modify Mark
 * Modify Time:  2019/10/19 18:49:02
 * Modify By:    a7498
 * Description:
 *
 *************************************************************/

namespace Library
{
    class Decrypt
    {
        public string Base64(string str)
        {
            return Encoding.Default.GetString((byte[])Convert.FromBase64String(str));
        }

        public string AES(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        public string DES(string str, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Convert.FromBase64String(str);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            try
            {
                cs.Write(inputByteArray, 0, inputByteArray.Length);
            }
            catch
            {
                return null;
            }
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
