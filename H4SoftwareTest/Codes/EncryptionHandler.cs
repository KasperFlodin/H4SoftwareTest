using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace H4SoftwareTest.Codes
{
    public class EncryptionHandler
    {
        private readonly IDataProtector _dataProtector;
        private static string _privatKey;
        private static string _publicKey;
        private readonly HttpClient _httpClient;


        public EncryptionHandler(IDataProtectionProvider dataProtector, HttpClient httpClient)
        {
            _dataProtector = dataProtector.CreateProtector("Passw0rd");

            string filePath = "keys.txt";

            if (File.Exists(filePath))
            {
                // Hvis filen eksisterer, læs nøglerne fra filen
                string[] keys = File.ReadAllLines(filePath);
                _privatKey = keys[0];
                _publicKey = keys[1];
            }
            else
            {
                // Hvis filen ikke eksisterer, generer nye nøgler og gem dem i filen
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    _privatKey = rsa.ToXmlString(true);
                    _publicKey = rsa.ToXmlString(false);
                }
                using (StreamWriter outputFile = new StreamWriter("keys.txt"))
                {
                    outputFile.WriteLine(_privatKey + "\b" + _publicKey);
                }
                File.WriteAllLines(filePath, new[] { _privatKey, _publicKey });
            }


            _httpClient = httpClient;
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //_privatKey = rsa.ToXmlString(true);
            //_publicKey = rsa.ToXmlString(false);
        }

        #region Symetric encryption

        public string EncryptSymetrics(string txtToEncrypt) => _dataProtector.Protect(txtToEncrypt);

        public string DecryptSymetrics(string txtToEncrypt) => _dataProtector.Unprotect(txtToEncrypt);

        #endregion

        #region Asymetric encryption

        //public async Task<string> EncryptAsymetriscParrent(string txtToEcrypt)
        //{

        //    string[] data = new string[2] { txtToEcrypt, _publicKey };
        //    string serializedValue = JsonConvert.SerializeObject(data);
        //    StringContent content = new StringContent(serializedValue, System.Text.Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync("https://localhost:7016/api/Encrypt", content);
        //    string encryptedValue = await response.Content.ReadAsStringAsync();
        //    return encryptedValue;
        //}



        // dette er vores web api code
        public string EncryptAsymetrisc(string txtToEncrypt)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);

            byte[] txtToEncryptAsByteArray = System.Text.Encoding.UTF8.GetBytes(txtToEncrypt);
            byte[] encryptedValue = rsa.Encrypt(txtToEncryptAsByteArray, true);

            string encryptedValueAsString = Convert.ToBase64String(encryptedValue);
            return encryptedValueAsString;

        }

        public string DecryptAsymetrisc(string txtToDecrypt)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_privatKey);

            byte[] txtToEncryptAsByteArray = System.Text.Encoding.UTF8.GetBytes(txtToDecrypt);

            byte[] txtToDecryptAsByteArray = Convert.FromBase64String(txtToDecrypt); 
            byte[] encryptedValue = rsa.Decrypt(txtToDecryptAsByteArray, true);
            string encryptedValueAsString = Encoding.UTF8.GetString(encryptedValue);

            return encryptedValueAsString;
        }
        #endregion


    }
}
