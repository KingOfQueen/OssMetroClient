using Caliburn.Micro;
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
    public class LeftViewModel : Conductor<ILeftWorkSpace>.Collection.OneActive, ILeftView
    {
        [ImportingConstructor]
        public LeftViewModel([ImportMany]IEnumerable<ILeftWorkSpace> workspaces)
        {
            Items.AddRange(workspaces);
            ActivateItem(workspaces.First());
        }

    }
}
