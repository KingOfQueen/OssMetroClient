using Oss;
using OssClientMetro.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace OssClientMetro.Model
{
    public class FileModel : ObjectModel
    {
        public FileModel()
        {

        }


        public void initial()
        {
            try
            {
                modifyTimeVisible = true;
                string[] fileNames = key.Split('/');
                displayName = fileNames[fileNames.Length - 1];
                setIcon();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




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

        public void setIcon()
        {
            try
            {
                Icon icon = null;
                string[] fileNams = key.Split('.');
                string fileType = null;
                if (fileNams.Length > 1)
                {
                    fileType = "." + fileNams[fileNams.Length - 1];
                    if(iconsInfo[fileType] != null)
                    {
                        string fileAndParam = (iconsInfo[fileType]).ToString();
                        icon = RegisteredFileType.ExtractIconFromFile(fileAndParam, true);
                    }
                }

                if (icon != null)
                {
                    IconUri = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                     icon.ToBitmap().GetHbitmap(),
                     IntPtr.Zero,
                     System.Windows.Int32Rect.Empty,
                     BitmapSizeOptions.FromWidthAndHeight(300, 300));

                    // IconUri = ScreenCapture.UriSource;
                }
                else //if the icon is invalid, show an error image.
                    IconUri = new Uri("pack://application:,,,/Images/fileDefault.png");
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}
