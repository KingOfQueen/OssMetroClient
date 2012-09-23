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


     [Export(typeof(IRightView))]
    class RightViewModel : Conductor<IRightWorkSpace>.Collection.OneActive, IRightView, IHandle<LoginResultEvent>, IHandle<DownloadViewEvent>, IHandle<BuketSelectedEvent>
    {
        readonly IEventAggregator events;
        readonly IClientService clientService;
        readonly IWindowManager windowManager;
        readonly IFileFolderDialogService fileFolderDialogService;

        [ImportingConstructor]
         public RightViewModel(IEventAggregator _events, IClientService _clientService,
            IWindowManager _windowManager, IFileFolderDialogService _fileFolderDialogService)
         {
              windowManager = _windowManager;
              events = _events;
              events.Subscribe(this);
              fileFolderDialogService = _fileFolderDialogService;    
              clientService = _clientService;
              welcomeViewModel = new WelcomeViewModel();
              ActivateItem(welcomeViewModel);
         }

        public void Handle(LoginResultEvent loginResult)
        {
            if (loginResult.result == Result.SUCCESS)
            {
                objectViewModel = new ObjectViewModel(events, clientService, windowManager, fileFolderDialogService);
                downloadViewModel = new DownloadViewModel(events, clientService, windowManager);
                ActivateItem(objectViewModel);
            }

        }

        public void Handle(DownloadViewEvent dowloadViewEvent)
        {
            ActivateItem(downloadViewModel);
        }

        public void Handle(BuketSelectedEvent buketSelectedEvent)
        {
            ActivateItem(objectViewModel);
        }

        DownloadViewModel downloadViewModel;
        ObjectViewModel objectViewModel;
        WelcomeViewModel welcomeViewModel;

    }


}
