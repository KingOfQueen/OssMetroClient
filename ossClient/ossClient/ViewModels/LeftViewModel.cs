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
    [Export(typeof(ILeftView))]
    public class LeftViewModel : Conductor<ILeftWorkSpace>.Collection.OneActive, ILeftView, IHandle<LoginResultEvent>
    {
          readonly IEventAggregator events;
        readonly IClientService clientService;

        [ImportingConstructor]
        public LeftViewModel(IEventAggregator _events, IClientService _clientService)
        {
            events = _events;
            events.Subscribe(this);
            clientService = _clientService;
            loginViewModel = new LoginViewModel(_events, _clientService);
            ActivateItem(loginViewModel);
        }

        LoginViewModel loginViewModel;
        NavigateViewModel navigateViewModel;


        public void Handle(LoginResultEvent loginResult)
        {
            if (loginResult.result == Result.SUCCESS)
            {
                navigateViewModel = new NavigateViewModel(events, clientService);
                ActivateItem(navigateViewModel);
            }

        }


    }
}
