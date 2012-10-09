using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Oss;
using System.IO;
using OssClientMetro.Model;
using System.Threading.Tasks;
using OssClientMetro.Framework;
using System.ComponentModel.Composition;
using OssClientMetro.Services;
using System.Windows;


namespace OssClientMetro
{
    [Export(typeof(IShell))]
    class MainWindowViewModel : PropertyChangedBase, IShell
    {
        [ImportingConstructor]
        public MainWindowViewModel(ILeftView firstViewModel, IRightView secondviewModel, IWindowManager _windowManager) // etc, for each child ViewModel
        {
            LeftView = firstViewModel;
            RightView = secondviewModel;
            windowManager = _windowManager;
        }

        IWindowManager windowManager;
        private int _overlayDependencies = 0;
        public void ShowOverlay()
        {
            _overlayDependencies++;
            NotifyOfPropertyChange(() => IsOverlayVisible);
        }

        public void HideOverlay()
        {
            _overlayDependencies--;
            NotifyOfPropertyChange(() => IsOverlayVisible);
        }

        public bool IsOverlayVisible
        {
            get { return _overlayDependencies > 0; }
        }


        public ILeftView LeftView { get; private set; }
        public IRightView RightView { get; private set; }

        public void loginOut()
        {
            windowManager.ShowMetroMessageBox("Are you sure you want to delete...", "Confirm Delete",
                                   MessageBoxButton.YesNo);
            UserInfoFile.saveFile(new User("", ""));

            Properties.Settings.Default.AutoLogin = false;
            Properties.Settings.Default.RememberKey = false;
            Properties.Settings.Default.Save();
            App.Current.Shutdown();
        }

    }
}
