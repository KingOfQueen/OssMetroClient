using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OssClientMetro.Model
{
    public class FileModel : ObjectModel
    {
        public DateTime modifyTime { get; set; }

        public long lastSize = 0;

        void timer_Tick(object sender, EventArgs e)
        {
            factory.StartNew(() =>
            {
                Speed = ProcessSize - lastSize;
                lastSize = ProcessSize;
                if (Percent == 100)
                {
                    timer.Stop();
                }
            });
           
        }

       public  void startTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }


        override public void callback(HttpProcessData httpProcessData)
        {
            factory.StartNew((stateobj) =>
            {
                HttpProcessData data = (HttpProcessData)stateobj;
                Percent = data.ProgressPercentage;
                Size = data.TotalBytes;
                ProcessSize = data.BytesTransferred;
         
            }, httpProcessData);
        }

    }
}
