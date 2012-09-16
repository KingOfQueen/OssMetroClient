using Caliburn.Micro;
using OssClientMetro.Events;
using OssClientMetro.Framework;
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
            userName = "bm9crcnr0rtnuw8bnrfvq7w8";
            userPassword = "RbtJoExTnA8vYLynUfDh7Ior+oM=";

        }

        private string userName;
        private string userPassword;


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


        public  async void login()
        {
            try
            {
                await clientService.login(UserName, UserPassword);
            }
            catch (Exception ex)
            {

            }

            events.Publish(new LoginResultEvent(Result.SUCCESS, null));
        }
    }
}
