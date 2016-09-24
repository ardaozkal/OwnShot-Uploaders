using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OwnShotImageBin
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient())
            {
                byte[] ImageBinResponse = client.UploadFile("https://imagebin.ca/upload.php", args[0]);
                var StringResponse = Encoding.UTF8.GetString(ImageBinResponse);
                File.AppendAllText("OwnShotImageBinLog.txt", StringResponse + Environment.NewLine);

                if (StringResponse.Contains("error") || StringResponse.Contains("fail"))
                {
                    Console.WriteLine("ERROR: failed. Read OwnShotImageBinLog.txt file for more info.");
                }
                else
                {
                    var link = "https" + StringResponse.Substring(StringResponse.LastIndexOf(":")); //Lazy method to not split, but meh, it turns the link to https so I'm fine with this
                    Console.WriteLine(link);
                }
            }
        }
    }
}
