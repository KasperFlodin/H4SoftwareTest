using BCrypt.Net;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace H4SoftwareTest.Codes;

public class HashingHandler
{
    public string MDHashing(string txtToHash)
    {
        MD5 md5 = MD5.Create();
        byte[] txtToHashAsByteArray = Encoding.ASCII.GetBytes(txtToHash);
        byte[] hashedValue = md5.ComputeHash(txtToHashAsByteArray);

        string hashedValueAsString = Convert.ToBase64String(hashedValue);
        return hashedValueAsString;
    }

    public string SHA2Hashing(string txtToHash)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] txtToHashAsByteArray = Encoding.ASCII.GetBytes(txtToHash);
        byte[] hashedValue = sha256.ComputeHash(txtToHashAsByteArray);

        string hashedValueAsString = Convert.ToBase64String(hashedValue);
        return hashedValueAsString;
    }

    public string HMACHashing(string txtToHash)
    {
        byte[] myKey = Encoding.ASCII.GetBytes("Passw0rd");
        byte[] txtToHashAsByteArray = Encoding.ASCII.GetBytes(txtToHash);

        HMACSHA256 hmac = new HMACSHA256();
        hmac.Key = myKey;

        byte[] hashedValue = hmac.ComputeHash(txtToHashAsByteArray);

        string hashedValueAsString = Convert.ToBase64String(hashedValue);
        return hashedValueAsString;
    }

    public string PBKDF2Hashing(string txtToHash)
    {
        byte[] salt = Encoding.ASCII.GetBytes("Passw0rd");
        byte[] txtToHashAsByteArray = Encoding.ASCII.GetBytes(txtToHash);

        var hashAlgo = new System.Security.Cryptography.HashAlgorithmName("SHA256");
        int iteration = 10;
        int outputLenght = 32;

        byte[] hashedValue = Rfc2898DeriveBytes.Pbkdf2(txtToHashAsByteArray, salt, iteration, hashAlgo, outputLenght);
        string hashedValueAsString = Convert.ToBase64String(hashedValue);
        return hashedValueAsString;
    }

    public string BCryptHashing(string txtToHash)
    {
        // default string hvis byte[] ønskes skal den convertes.
        // example 1:
        //return BCrypt.Net.BCrypt.HashPassword(txtToHash);

        // example2
        // iteration
        int workFactor = 10;
        bool enchancedEntropi = true;
        return BCrypt.Net.BCrypt.HashPassword(txtToHash, workFactor, enchancedEntropi);


        //Example 3
        //string salt = BCrypt.Net.BCrypt.GenerateSalt();
        //bool enchancedEntropi = true;
        //HashType hashType = HashType.SHA256;

        //return BCrypt.Net.BCrypt.HashPassword(txtToHash, salt, enchancedEntropi, hashType);

    }

    public bool BCryptHashingVerify(string txtToHash, string hashedValueAsString)
    {
        // example 1 verify
        //return BCrypt.Net.BCrypt.Verify(txtToHash, hashedValueAsString);

        // example 2 verify
        return BCrypt.Net.BCrypt.Verify(txtToHash, hashedValueAsString, true);

        // example 3 verify
        //return BCrypt.Net.BCrypt.Verify(txtToHash, hashedValueAsString, true, HashType.SHA256);

    }




}

