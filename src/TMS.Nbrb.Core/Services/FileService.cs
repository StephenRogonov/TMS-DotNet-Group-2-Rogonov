using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TMS.Nbrb.Core.Helpers;
using TMS.Nbrb.Core.Interfaces;

namespace TMS.Nbrb.Core.Services
{
    /// <inheritdoc cref="IFileService"/>
    public class FileService : IFileService
    {
        public async Task WriteToFileAsync(string text)
        {
            await WriteAsync(text, Constants.FileName);
        }

        public async Task WriteToFileAsync(string text, string path)
        {
            await WriteAsync(text, path);
        }

        private async Task WriteAsync(string text, string path)
        {
            try
            {
                using StreamWriter sw = new StreamWriter(path, true, Encoding.Default);
                await sw.WriteLineAsync(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
