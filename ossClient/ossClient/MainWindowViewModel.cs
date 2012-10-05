using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Oss;
using System.IO;
using OssClientMetro.Model;
using System.Threading.Tasks;
using NLog;
using OssClientMetro.Framework;
using System.ComponentModel.Composition;
using OssClientMetro.Services;


namespace OssClientMetro
{
    [Export(typeof(IShell))]
    class MainWindowViewModel :  IShell
    {
        [ImportingConstructor]
        public MainWindowViewModel(ILeftView firstViewModel, IRightView secondviewModel) // etc, for each child ViewModel
        {
            LeftView = firstViewModel;
            RightView = secondviewModel;
        }

        public ILeftView LeftView { get; private set; }
        public IRightView RightView { get; private set; }

        public void loginOut()
        {
            UserInfoFile.saveFile(new User("", ""));

            Properties.Settings.Default.AutoLogin = false;
            Properties.Settings.Default.RememberKey = false;
            Properties.Settings.Default.Save();
            App.Current.Shutdown();
        }

    }
}
