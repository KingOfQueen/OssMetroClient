using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Services
{
    class History : PropertyChangedBase
    {

        public History()
        {
            nowPos = -1;
            canGoBack = false;
            canGoForward = false;
            paths = new List<string>();
        }

        void add(string path)
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
            }

        }


        public string getNowPath()
        {
            if (nowPos != -1)
                return paths[nowPos];

            return null;
        }


        public List<string> paths;

        int nowPos;


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
