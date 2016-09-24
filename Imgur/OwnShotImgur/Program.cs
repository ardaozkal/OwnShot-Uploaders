using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace OwnShotImgur
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var wc = new WebClient())
            {
                var values = new NameValueCollection{ {"image", Convert.ToBase64String(File.ReadAllBytes(args[0]))} };

                wc.Headers.Add("Authorization", "Client-ID e25a7c14cc23579");
                byte[] ImgurResponse = wc.UploadValues("https://api.imgur.com/3/upload.xml", values);
                var StringResponse = System.Text.Encoding.UTF8.GetString(ImgurResponse);
                File.AppendAllText("OwnShotImgurLog.txt", StringResponse+Environment.NewLine);

                if (!StringResponse.Contains("success") || !StringResponse.Contains("<link>"))
                {
                    Console.WriteLine("ERROR: failed. Read OwnShotImgurLog.txt file for more info.");
                }
                else
                {
                    var link = StringResponse.Substring(StringResponse.IndexOf("<link>") + 6);
                    link = link.Substring(0, link.IndexOf("</link>")); //I'm too lazy to parse xml
                    Console.WriteLine(link);
                }
            }
        }
    }
}
