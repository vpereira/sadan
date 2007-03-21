using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace RSACreateKeys
{
    class Program
    {
        // Declare CspParmeters and RsaCryptoServiceProvider
        // objects with global scope of your Form class.
        //static CspParameters cspp = new CspParameters();
        //static RSACryptoServiceProvider rsa;

        // Path variables for source, encryption, and
        // decryption folders. Must end with a backslash.
        const string EncrFolder = @"c:\Encrypt\";
        const string DecrFolder = @"c:\Decrypt\";
        const string SrcFolder = @"c:\docs\";

        // Public key file
        const string PubKeyFile = @"c:\encrypt\rsaPublicKey.xml";
        const string PrivKeyFile = @"C:\encrypt\rsaPrivKey.xml";

        // Key container name for
        // private/public key value pair.
        const string keyName = "sadan01";


        static void CreateRSA(string keyName)
        {
            CspParameters cspp = new CspParameters();
            RSACryptoServiceProvider rsa;
            cspp.KeyContainerName = keyName;
            //cspp.KeyContainerName = keyName;

            try
            {
                rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        static void ExportPublicKey(string keyName)
        {
            CspParameters mCsp = new CspParameters();
            RSACryptoServiceProvider rsa;
            mCsp.KeyContainerName = keyName;

            try
            {
                rsa = new RSACryptoServiceProvider(mCsp);
                StreamWriter sw = new StreamWriter(PubKeyFile);
                sw.Write(rsa.ToXmlString(false));
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        static void ExportPrivKey(string keyName)
        {
            CspParameters mCsp = new CspParameters();
            RSACryptoServiceProvider rsa;
            mCsp.KeyContainerName = keyName;

            try
            {
                rsa = new RSACryptoServiceProvider(mCsp);
                StreamWriter sw = new StreamWriter(PrivKeyFile);
                sw.Write(rsa.ToXmlString(true));
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void LoadPublicKey(string PubKeyFile, string keyName)
        {
            CspParameters cspp = new CspParameters();
            StreamReader sr = new StreamReader(PubKeyFile);
            bool bFullPair = false;

            cspp.KeyContainerName = keyName;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
            
            string keytxt = sr.ReadToEnd();
            rsa.FromXmlString(keytxt);
            rsa.PersistKeyInCsp = true;
            sr.Close();

            if (!IsFullPair(keyName))
            {
                Console.WriteLine("Load from {0} was successful", PubKeyFile);
            }
        }

        public static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        static bool IsFullPair(string keyName)
        {
            CspParameters mCsp = new CspParameters();
            RSACryptoServiceProvider rsa;

            mCsp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(mCsp);
            if (rsa.PublicOnly == true)
                return false;
            else
                return true;

        }

        static byte [] EncryptData(string data)
        {
           // CspParameters cspp = new CspParameters();
            RSACryptoServiceProvider rsa;
            //cspp.KeyContainerName = keyName;
            byte [] nByte = new byte[1024];

            try
            {
                rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(
                rsa.PersistKeyInCsp = true;
                nByte = StrToByteArray(data);
                rsa.Encrypt(nByte, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return nByte;
        }
        static string DecryptData(byte [] data)
        {
            CspParameters cspp = new CspParameters();
            RSACryptoServiceProvider rsa;
            cspp.KeyContainerName = keyName;
            byte [] nByte = new byte[1024];
            string rString = null;
            //cspp.KeyContainerName = keyName;

            try
            {
                rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;
                nByte = rsa.Decrypt(data, false);
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                rString = enc.GetString(nByte);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rString;
           
        }
        static void Main(string[] args)
        {
            bool bFullPair = false;
            string datain = "ABC123";
            byte[] dataout = new byte[1024];

            CreateRSA(keyName);
            bFullPair = IsFullPair(keyName);

            if (bFullPair != true)
                Console.WriteLine("Key: " + keyName + " - Public Only");
            else
                Console.WriteLine("Key: " + keyName + " - Full Key Pair");

            Console.WriteLine("Exporting Public Key to XML: {0} ", PubKeyFile);
            ExportPublicKey(keyName);

            if (bFullPair == true)
            {
                Console.WriteLine("Exporting Private Key to XML: {0} ", PrivKeyFile);
                ExportPrivKey(keyName);
            }

          
            Console.WriteLine("Testing the class...");
            Console.WriteLine("Encrypting data: {0}",datain);
            dataout = EncryptData(datain);
            Console.WriteLine("Decrypting data..");
            datain = DecryptData(dataout);
            Console.WriteLine("Data decrypted: {0}", datain);
        }
    }
}
