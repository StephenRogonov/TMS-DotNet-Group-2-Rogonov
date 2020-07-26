using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Nbrb.Core.Interfaces
{
    public interface IFileService
    {
        public void WriteToFile(string text);
        public void WriteToFile(string text, string path);
    }
}
