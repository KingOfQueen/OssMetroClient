using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    public class CreateFolderEvent
    {
        public CreateFolderEvent(string _folderName)
        {
            folderName = _folderName;
        }
        public string folderName;
    }
}
