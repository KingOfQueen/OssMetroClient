using Oss;
using OssClientMetro.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static string lastName(string _key)
        {
            string[] fileNames = _key.Split('/');
            return fileNames[fileNames.Length - 1];
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
                    switch (fileType)
                    {
                        case ".rar":
                            IconUri = new Uri("pack://application:,,,/Images/rar.png");
                            break;
                        case ".avi":
                            IconUri = new Uri("pack://application:,,,/Images/avi.png");
                            break;
                        case ".dll":
                            IconUri = new Uri("pack://application:,,,/Images/dll.png");
                            break;
                        case ".dmg":
                            IconUri = new Uri("pack://application:,,,/Images/dmg.png");
                            break;
                        case ".exe":
                            IconUri = new Uri("pack://application:,,,/Images/exe.png");
                            break;
                        case ".flv":
                            IconUri = new Uri("pack://application:,,,/Images/flv.png");
                            break;
                        case ".gif":
                            IconUri = new Uri("pack://application:,,,/Images/gif.png");
                            break;
                        case ".mov":
                            IconUri = new Uri("pack://application:,,,/Images/mov.png");
                            break;
                        case ".mpg":
                            IconUri = new Uri("pack://application:,,,/Images/mpg.png");
                            break;
                        case ".pdf":
                            IconUri = new Uri("pack://application:,,,/Images/pdf.png");
                            break;
                        case ".png":
                            IconUri = new Uri("pack://application:,,,/Images/png.png");
                            break;
                        case ".ppt":
                        case ".pptx":
                            IconUri = new Uri("pack://application:,,,/Images/powerpoint.png");
                            break;
                        case ".doc":
                        case ".docx":
                            IconUri = new Uri("pack://application:,,,/Images/word.png");
                            break;
                        case ".xls":
                        case ".xlsx":
                            IconUri = new Uri("pack://application:,,,/Images/excel.png");
                            break;

                        case ".psd":
                            IconUri = new Uri("pack://application:,,,/Images/psd.png");
                            break;
                        case ".scv":
                            IconUri = new Uri("pack://application:,,,/Images/scv.png");
                            break;

                        case ".wma":
                            IconUri = new Uri("pack://application:,,,/Images/wma.png");
                            break;


                        case ".zip":
                            IconUri = new Uri("pack://application:,,,/Images/zip.png");
                            break;
                        case ".jpg":
                            IconUri = new Uri("pack://application:,,,/Images/jpg.png");
                            break;

                  }
                }

                if (IconUri == null)
                    IconUri = new Uri("pack://application:,,,/Images/file.png");
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}
