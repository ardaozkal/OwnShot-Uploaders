using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OwnShotQuadFile
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient())
            {
                byte[] ImageBinResponse = client.UploadFile("https://file.quad.moe", args[0]);
                var StringResponse = Encoding.UTF8.GetString(ImageBinResponse);
                File.AppendAllText("OwnShotQuadFileLog.txt", StringResponse + Environment.NewLine);

                if (StringResponse.Contains("error") || StringResponse.Contains("fail"))
                {
                    Console.WriteLine("ERROR: failed. Read OwnShotQuadFileLog.txt file for more info.");
                }
                else
                {
                    var link = "https" + StringResponse.Substring(StringResponse.LastIndexOf(":")).Replace("\"}", ""); //Lazy method to not split, but meh, it turns the link to https so I'm fine with this
                    Console.WriteLine(link);
                }
            }
        }
    }
}
