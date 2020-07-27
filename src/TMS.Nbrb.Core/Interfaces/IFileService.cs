using System.Threading.Tasks;

namespace TMS.Nbrb.Core.Interfaces
{
    public interface IFileService
    {
        public Task WriteToFileAsync(string text);
        public Task WriteToFileAsync(string text, string path);
    }
}
