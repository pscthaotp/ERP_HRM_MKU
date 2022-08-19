using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.Commons
{
    public static class EncryptUtils
    {
        /// <summary>
        /// Encrypt password using SHA 512 algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPassword(string password)
        {
            if (!String.IsNullOrEmpty(password))
            {
                string result;
                SHA512Managed sha512 = new SHA512Managed();
                byte[] data = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                result = Convert.ToBase64String(data);
                return result;
            }
            return password;
        }

        /// <summary>
        /// Mã hóa 1 chuuỗi, có thể giải mã ngược lại
        /// </summary>
        /// <param name="toEncrypt">Chuỗi cần mã hóa</param>
        /// <param name="key">từ khóa dùng để mã hóa</param>
        /// <param name="useHashing">sử dụng MD5 để tạo key từ khóa</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            try
            {
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Giải mã ngược lại chuỗi đã mã hóa
        /// </summary>
        /// <param name="toDecrypt">Chuỗi mã hóa</param>
        /// <param name="key">từ khóa đã dùng để mã hóa</param>
        /// <param name="useHashing">sử dụng MD5 để tạo key từ khóa</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            try
            {
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }


        public static string EncryptMD5(string userName, string password)
        {
            try
            {
                string text = "UisStaffID=" + userName.ToUpper() + ";UisPassword=" + password;

                MD5 mh = MD5.Create();
                //Chuyển kiểu chuổi thành kiểu byte
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
                //mã hóa chuỗi đã chuyển
                byte[] hash = mh.ComputeHash(inputBytes);
                //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }

        }
    }
}
