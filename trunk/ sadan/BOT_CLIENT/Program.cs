using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using System.Net;

namespace BOT_CLIENT
{
    class Program
    {
        const string m_KeyName = "sadan01";
        const string m_pubKeyFile = "c:\\encrypt\\rsaPublicKey.xml";
        static bool m_Success = false;

        static void Main(string[] args)
        {
            RSACryptoServiceProvider rsa;
            string site;
            string jobid;
            string url;
            string xmlraw;

            if (args.Length != 2)
            {
                Console.WriteLine("GET REMOTE XML");
                Console.WriteLine("Victor Pereira <vp@sekure.org>");
                Console.WriteLine("GET.exe <site> <jobid>");
                Environment.Exit(1);
            }

            //In user input we trust
            site = args[0];
            jobid = args[1];
            xmlraw = jobid + ".xml";

            //URL EXAMPLE http://www.foobar.org/31337.html
            try
            {
                url = site + "/" + jobid + ".html";
                WebRequest wReq = WebRequest.Create(url);
                WebResponse wResp = wReq.GetResponse();
                StreamReader sr = new StreamReader(wResp.GetResponseStream(), Encoding.UTF8);
                StreamWriter sw = new StreamWriter(xmlraw);

                int length = 1024;
                char[] Buff = new char[1024];
                int bytesread = 0;

                bytesread = sr.Read(Buff, 0, length);

                while (bytesread > 0)
                {
                    sw.Write(Buff, 0, bytesread);
                    sw.WriteLine();
                    Console.Write(Buff, 0, length);
                    Console.WriteLine();
                    bytesread = sr.Read(Buff, 0, length);
                }

                //Console.WriteLine("\n");
                sw.Close();
                wResp.Close();


                rsa = LoadPublicKey(m_pubKeyFile, m_KeyName);
                XmlDocument xmlDoc = new XmlDocument();

                // Load an XML file into the XmlDocument object.
                xmlDoc.PreserveWhitespace = false;
                xmlDoc.Load(xmlraw);

                // Verify the signature of the signed XML.
                Console.WriteLine("Verifying signature...");

                bool result = VerifyXml(xmlDoc, rsa);

                // Display the results of the signature verification to 
                // the console.
                if (!result)
                {
                    Console.WriteLine("The XML signature is not valid.");
                    Environment.Exit(1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Verify the signature of an XML file against an asymmetric 
        // algorithm and return the result.
        public static Boolean VerifyXml(XmlDocument Doc, RSA Key)
        {
            // Check arguments.
            if (Doc == null)
                throw new ArgumentException("Doc");
            if (Key == null)
                throw new ArgumentException("Key");

            // Create a new SignedXml object and pass it
            // the XML document class.
            SignedXml signedXml = new SignedXml(Doc);

            // Find the "Signature" node and create a new
            // XmlNodeList object.
            XmlNodeList nodeList = Doc.GetElementsByTagName("Signature");

            // Throw an exception if no signature was found.
            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }

            //One Sig per document
            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }

            // Load the first <signature> node.  
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // Check the signature and return the result.
            return signedXml.CheckSignature(Key);
        }
        static RSACryptoServiceProvider LoadPublicKey(string PubKeyFile, string keyName)
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

            /*
            if (!IsFullPair(keyName))
            {
                Console.WriteLine("Public key was loaded from {0}!", PubKeyFile);
            }*/

            return rsa;
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
        private static void ValidationCallBack(Object sender, ValidationEventArgs args)
        {
            //Display the validation error.  This is only called on error
            m_Success = false; //Validation failed
            //Console.WriteLine("Validation error: " + args.Message);
        }
        private static bool validateXml(String infile)
        {
            //First we create the xmltextreader
            XmlTextReader xmlr = new XmlTextReader(infile);
            //We pass the xmltextreader into the xmlvalidatingreader
            //This will validate the xml doc with the schema file
            //NOTE the xml file it self points to the schema file
            XmlValidatingReader xmlvread = new XmlValidatingReader(xmlr);

            // Set the validation event handler
            xmlvread.ValidationEventHandler +=
                new ValidationEventHandler(ValidationCallBack);
            m_Success = true; //make sure to reset the success var

            // Read XML data
            while (xmlvread.Read()) { }

            //Close the reader.
            xmlvread.Close();

            //The validationeventhandler is the only thing that would set 
            //m_Success to false
            return m_Success;
        }
    }
}
