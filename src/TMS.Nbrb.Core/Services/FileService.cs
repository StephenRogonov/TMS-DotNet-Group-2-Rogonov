using System;
using System.IO;
using System.Text;
using TMS.Nbrb.Core.Helpers;
using TMS.Nbrb.Core.Interfaces;

namespace TMS.Nbrb.Core.Services
{
    public class FileService : IFileService
    {
        public void WriteToFile(string text)
        {
            WriteAsync(text, Constants.path);
        }

        public void WriteToFile(string text, string path)
        {
            WriteAsync(text, path);
        }

        private async void WriteAsync(string text, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                {
                    await sw.WriteLineAsync(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
