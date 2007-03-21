using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace XMLPARSE
{
    class Program
    {
        static bool m_Success = false;

        static void Main(string[] args)
        {
            string file = "sadan_xml.xml";

            try
            {
                m_Success = validateXml(file);

                if (m_Success)
                    Console.WriteLine("XML CORRETO");
                else
                    Console.WriteLine("XML INCORRETO");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

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
