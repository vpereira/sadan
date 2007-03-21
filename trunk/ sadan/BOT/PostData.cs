using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Web;


namespace BOT
{
    class PostData
    {
        //Where we store our Key Information
        const string m_KeyName = "sadan01";

        //Set it and your screen will be flooded by useless information :-P
        const int m_debug = 0;

        static void Main(string[] args)
        {
            string site;
            string jobid;
            string xmlin;
            string xmltmp = "sadan_tmp.xml";
            RSACryptoServiceProvider rsa;


            if (args.Length != 3)
            {
                Console.WriteLine("POST A SIGNED XML AND JOB TO A SITE");
                Console.WriteLine("Victor Pereira - vp@sekure.org");
                Console.WriteLine("post.exe <site> <jobid> <xmlin>");
                Environment.Exit(0);
            }

            //yeah, yeah, yeah in user input i trust.
            site = args[0];
            jobid = args[1];
            xmlin = args[2];

            if (!File.Exists(xmlin))
            {
                Console.WriteLine("You must choose a file in...");
                Environment.Exit(1);
            }


            rsa = LoadPrivateKey(m_KeyName);

            if (rsa.PublicOnly == true)
            {
                Console.WriteLine("Did you generated the Key pair ?");
            }
            else
            {
                Console.WriteLine("Private Key loaded!!");
            }

            // Create a new XML document.
            XmlDocument xmlDoc = new XmlDocument();

            // Load an XML file into the XmlDocument object.
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.Load(xmlin);

            // Sign the XML document. 
            SignXml(xmlDoc, rsa);

            //Console.WriteLine(xmlDoc.ToString());
            Console.WriteLine("XML file signed.");

            // Save the document.
            xmlDoc.Save(xmltmp);
            MyPostData(xmltmp, site, jobid);
            Console.WriteLine("XML WAS POSTED!");
        }

        static void MyPostData(string infile, string site, string jobid)
        {
            StringBuilder SBData = new StringBuilder();

            SBData.Append("jobid=");
            SBData.Append(jobid);
            SBData.Append("&message=");

            StreamReader sr = new StreamReader(infile);
           
            while (sr.Peek() >= 0)
            {
                //Let's do the URL ENCODE
                SBData.Append(HttpUtility.UrlEncode(sr.ReadLine()));
                //SBData.AppendLine(sr.ReadLine());
            }

            sr.Close();

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(SBData.ToString());

            // Prepare web request...
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(site);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();
                // Send the data.
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                if (m_debug == 1)
                {

                    //Reading the response..
                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    StreamReader ss = new StreamReader(myResponse.GetResponseStream(), Encoding.ASCII);

                    int length = 1024;
                    char[] Buff = new char[1024];
                    int bytesread = 0;

                    bytesread = ss.Read(Buff, 0, length);

                    while (bytesread > 0)
                    {
                        Console.Write(Buff, 0, length);
                        bytesread = ss.Read(Buff, 0, length);
                    }
                    Console.WriteLine("\n");
                    myResponse.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static RSACryptoServiceProvider LoadPrivateKey(string keyName)
        {
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = keyName;

            // Create a new RSA signing key and save it in the container. 
            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);
            rsaKey.PersistKeyInCsp = true;
            return rsaKey;
        }
        static void SignXml(XmlDocument Doc, RSA Key)
        {
            // Check arguments.
            if (Doc == null)
                throw new ArgumentException("Doc");
            if (Key == null)
                throw new ArgumentException("Key");

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(Doc);

            // Add the key to the SignedXml document.
            signedXml.SigningKey = Key;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            Doc.DocumentElement.AppendChild(Doc.ImportNode(xmlDigitalSignature, true));
        }
    }
}
