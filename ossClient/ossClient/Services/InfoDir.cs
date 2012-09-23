using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Services
{
    class InfoDir
    {
        static public DirectoryInfo getClientDir()
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\OssMetroClient\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return new DirectoryInfo(path);

        }

    }
}
