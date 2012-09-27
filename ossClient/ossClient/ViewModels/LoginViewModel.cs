using Caliburn.Micro;
using OssClientMetro.Events;
using OssClientMetro.Framework;
using OssClientMetro.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.ViewModels
{
    class LoginViewModel : PropertyChangedBase, ILeftWorkSpace
    {
         readonly IEventAggregator events;
        readonly IClientService clientService;

        public LoginViewModel(IEventAggregator _events, IClientService _clientService)
        {
            events = _events;
            clientService = _clientService;
            initUserData();
            if (AutoLogin)
            {
                login();
            }
            

        }

        void initUserData()
        {
            User user = UserInfoFile.readFile();
            userName = user.UserName;
            userPassword = user.UserPassword;
            if (userName == "")
            {
                RememberKey = false;
                AutoLogin = false;
            }
            else
            {
                RememberKey = Properties.Settings.Default.RememberKey;
                AutoLogin = Properties.Settings.Default.AutoLogin;
            }
        }

        private string userName;
        private string userPassword;
        private bool autoLogin;
        private bool rememberKey;
        private bool progressActive;
        private string status;


        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
                NotifyOfPropertyChange(() => this.UserName);
            }
        }

        public string UserPassword
        {
            get
            {
                return this.userPassword;
            }
            set
            {
                this.userPassword = value;
                NotifyOfPropertyChange(() => this.UserPassword);
            }
        }
        

        public bool RememberKey
        {
            get
            {
                return this.rememberKey;
            }
            set
            {
                this.rememberKey = value;
                NotifyOfPropertyChange(() => this.RememberKey);
            }
        }

        public bool AutoLogin
        {
            get
            {
                return this.autoLogin;
            }
            set
            {
                this.autoLogin = value;
                NotifyOfPropertyChange(() => this.AutoLogin);
            }
        }

        public bool ProgressActive
        {
            get
            {
                return this.progressActive;
            }
            set
            {
                this.progressActive = value;
                NotifyOfPropertyChange(() => this.ProgressActive);
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
                NotifyOfPropertyChange(() => this.Status);
            }
        }

        public  async void login()
        {
            try
            {
                ProgressActive = true;
                await clientService.login(UserName, UserPassword);
                events.Publish(new LoginResultEvent(Result.SUCCESS, null));
                saveData();
            }
            catch (Exception ex)
            {
                Status = ex.Message;
                ProgressActive = false;
                AutoLogin = false;
            }            
        }

        void saveData()
        {

            if (AutoLogin || RememberKey)
                UserInfoFile.saveFile(new User(userName, MemoryPassword.EncryptDES(userPassword)));
            else
                UserInfoFile.saveFile(new User(userName, ""));

            Properties.Settings.Default.AutoLogin = AutoLogin;
            Properties.Settings.Default.RememberKey = RememberKey;
            Properties.Settings.Default.Save();



        }


    }
}
