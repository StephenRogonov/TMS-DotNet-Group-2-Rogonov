using System.Threading.Tasks;

namespace TMS.Nbrb.Core.Interfaces
{
    /// <summary>
    /// Service for working with file system.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Write to file.
        /// </summary>
        /// <param name="text">Text.</param>
        Task WriteToFileAsync(string text);

        /// <summary>
        /// Write to file.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="path">Path.</param>
        Task WriteToFileAsync(string text, string path);
    }
}
