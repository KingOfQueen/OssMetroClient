using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    public enum DownloadViewEventType
    {
        DOWNLOADINGVIEW,
        UPLOADINGVIEW,
        COMPELETEDVIEW
    };



    public class DownloadViewEvent
    {
        public DownloadViewEvent(DownloadViewEventType _type)
        {
            type = _type;
        }
        public DownloadViewEventType type;

    }
}
