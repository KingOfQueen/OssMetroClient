using Caliburn.Micro;
using OssClientMetro.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OssClientMetro.ViewModels
{
    class CreateFolderViewModel : Screen
    {

        readonly IEventAggregator events;

        public CreateFolderViewModel(IEventAggregator _events)
        {
            events = _events;
        }

        string folderName;



        public string FolderName
        {
            get
            {
                return this.folderName;
            }
            set
            {
                this.folderName = value;
                NotifyOfPropertyChange(() => this.FolderName);
            }
        }

        public void Create()
        {
            events.Publish(new CreateFolderEvent(FolderName));
            this.TryClose();
        }

        public void Cancel()
        {
            this.TryClose();
        }

    }
}
