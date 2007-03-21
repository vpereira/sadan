using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;


namespace WebCLN
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest wReq = WebRequest.Create("http://vp.railsplayground.net/sadan_xml.xml");
            WebResponse wResp = wReq.GetResponse();
            StreamReader sr = new StreamReader(wResp.GetResponseStream(), Encoding.ASCII);

            int length = 1024;
            char[] Buff = new char[1024];
            int bytesread = 0;

            bytesread = sr.Read(Buff, 0, length);

            while (bytesread > 0)
            {
                Console.Write(Buff, 0, length);
                bytesread = sr.Read(Buff, 0, length);
            }
            Console.WriteLine("\n");
            wResp.Close();

        }
    }
}
