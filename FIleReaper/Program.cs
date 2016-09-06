using System;
using System.IO;
using System.Text;

namespace FIleReaper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No args! Need path to file.");
                Console.ReadKey();
                return;
            }
            var pathToFile = args[0];
            if (!File.Exists(pathToFile))
            {
                Console.WriteLine($"File {pathToFile} not exist");
                Console.ReadKey();
                return;
            }
            var reapedBytes = new byte[10*1024*1024];
            using (var fs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Position = fs.Length - reapedBytes.Length;
                fs.Read(reapedBytes, 0, reapedBytes.Length);
            }
            var reapedFileName = Path.GetFileName(pathToFile);
            File.WriteAllText(reapedFileName ?? "reapedData.txt", Encoding.UTF8.GetString(reapedBytes));
            Console.WriteLine("Success");
            Console.Read();
        }
    }
}
