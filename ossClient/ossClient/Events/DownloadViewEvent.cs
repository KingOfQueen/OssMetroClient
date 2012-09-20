using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    public enum BuketSelectedEventType
    {
        DOWNLOADINGVIEW,
        UPLOADINGVIEW,
        COMPELETEDVIEW,
        DEFALT
    };



    public class DownloadViewEvent
    {
        public DownloadViewEvent(BuketSelectedEventType _type)
        {
            type = _type;
        }
        public BuketSelectedEventType type;

    }
}
