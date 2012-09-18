using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Services
{
    public class History : PropertyChangedBase
    {

        public History()
        {
            nowPos = -1;
            canGoBack = false;
            canGoForward = false;
            paths = new List<string>();
        }

        public void add(string path)
        {
            if (nowPos == paths.Count() - 1)
            {
                paths.Add(path);
                nowPos++;


                if (nowPos > 0)
                {
                    CanGoBack = true;
                }
            }
            else
            {
                nowPos++;
                paths[nowPos] = path;

                paths.RemoveRange(nowPos + 1, paths.Count() -1 - nowPos);
                CanGoBack = true;
            }
            NowPath = paths[nowPos];
            CanGoForward = false;
        }

        public void goBack()
        {
            if (CanGoBack)
            {
                nowPos--;
                if (nowPos == 0)
                    CanGoBack = false;

                CanGoForward = true;
                NowPath = paths[nowPos];
            }

        }

        public void goForward()
        {
            if (CanGoForward)
            {
                nowPos++;
                if (nowPos == paths.Count() - 1)
                    CanGoForward = false;

             
                CanGoBack = true;

                NowPath = paths[nowPos];
            }

        }



        public List<string> paths;

        int nowPos;

        string nowPath;

        public string NowPath
        {
            get
            {
                return this.nowPath;
            }
            set
            {
                this.nowPath = value;
                NotifyOfPropertyChange(() => this.NowPath);
            }
        }


        bool canGoBack;
        bool canGoForward ;

        public bool CanGoBack
        {
            get
            {
                return this.canGoBack;
            }
            set
            {
                this.canGoBack = value;
                NotifyOfPropertyChange(() => this.CanGoBack);
            }
        }

        public bool CanGoForward
        {
            get
            {
                return this.canGoForward;
            }
            set
            {
                this.canGoForward = value;
                NotifyOfPropertyChange(() => this.CanGoForward);
            }
        }

    }
}
