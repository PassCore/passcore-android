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

    public class Encrypt
    {

        /*public string MD5(string str, int num)
        {
            if (num % 2 != 0 | num > 32)
            {
                throw new ArgumentException("");
            }
            else
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                return BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str))).Replace("-", "").ToLower().Substring(num / 2, num);
            }
        }*/

        public string MD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str))).Replace("-", "").ToLower();
        }
        

        public string SHA512(string str)
        {
            SHA512CryptoServiceProvider sha512 = new SHA512CryptoServiceProvider();
            return BitConverter.ToString((byte[])sha512.ComputeHash((byte[])Encoding.UTF8.GetBytes(str))).Replace("-", string.Empty).ToLower();
        }

        public string SHA256(string str)
        {
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            return BitConverter.ToString((byte[])sha256.ComputeHash((byte[])Encoding.UTF8.GetBytes(str))).Replace("-", string.Empty).ToLower();
        }

        public string SHA384(string str)
        {
            SHA384CryptoServiceProvider sha384 = new SHA384CryptoServiceProvider();
            return BitConverter.ToString((byte[])sha384.ComputeHash((byte[])Encoding.UTF8.GetBytes(str))).Replace("-", string.Empty).ToLower();
        }

        public string SHA1(string str)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString((byte[])sha1.ComputeHash((byte[])Encoding.UTF8.GetBytes(str))).Replace("-", string.Empty).ToLower();
        }

        public string Base64(string str)
        {
            return Convert.ToBase64String((byte[])Encoding.Default.GetBytes(str));
        }


        public string AES(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string DES(string str, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(str);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var retB = Convert.ToBase64String(ms.ToArray());
            return retB;
        }
    }
}
