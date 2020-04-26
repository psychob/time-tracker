using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.StorageProvider
{
    interface IStorageProvider
    {
        bool IsMultiFile();
        List<string> GetSupportedExtensions();
    }
}
