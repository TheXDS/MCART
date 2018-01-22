using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheXDS.MCART.Networking.DownloadHelper;
using System.IO;

namespace Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://ipv4.download.thinkbroadband.com/5MB.zip";
            MemoryStream ms = new MemoryStream();
            DownloadHttpAsync(new Uri(url), ms, Report, 100).GetAwaiter().GetResult();
            Console.ReadKey();
        }



        static void Report(long? current,long total)
        {
            Console.WriteLine($"{current}/{total}");
        }
    }
}
