using System;
using System.Collections.Generic;
using System.Net;

namespace RemitaGenRRRStatus
{
    /// <summary>
    /// 
    /// </summary>
    public class WebClientUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="GET"></param>
        /// <param name="BaseURL"></param>
        /// <param name="APIMethod"></param>
        /// <param name="_Headers"></param>
        /// <returns></returns>
        public static string GetResponse(String BaseURL, String APIMethod, Headers _Headers)
        {
            Console.WriteLine("+++++++++ URL: " + $"{BaseURL}{APIMethod}");

            String response = string.Empty;
            try
            {
                var client = new WebClient();
                
                foreach (var i in _Headers.headers)
                    client.Headers.Add(i.header, i.value);
                response = client.DownloadString($"{BaseURL}{APIMethod}");
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="POST"></param>
        /// <param name="BaseURL"></param>
        /// <param name="APIMethod"></param>
        /// <param name="_Headers"></param>
        /// <returns></returns>
        public static string PostResponse(String BaseURL, String APIMethod, String body, Headers _Headers)
        {
            Console.WriteLine("+++++++++ URL: " + $"{BaseURL}{APIMethod}");

            Console.WriteLine();
            Console.WriteLine("++++++++++++++Body: " + body);
            Console.WriteLine();

            String response = string.Empty;
            try
            {
                var client = new WebClient();
                
                foreach (var i in _Headers.headers)
                    client.Headers.Add(i.header, i.value);

                client.Encoding = System.Text.Encoding.UTF8;
                string method = "POST";

                response = client.UploadString($"{BaseURL}{APIMethod}", method, body);
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// API Header Object
        /// </summary>
        public class Header
        {
            /// <summary>
            /// header
            /// </summary>
            public string header { get; set; }
            /// <summary>
            /// value
            /// </summary>
            public string value { get; set; }
        }

        /// <summary>
        /// API Headers Object 
        /// </summary>
        public class Headers
        {
            /// <summary>
            /// Header List Object
            /// </summary>
            public List<Header> headers { get; set; }
        }

        public static string SHA512(string hash_string)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            string hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            return hashed;
        }
    }
}
