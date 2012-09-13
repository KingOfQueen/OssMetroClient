using Caliburn.Micro;
using ossClient.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ossClient.ViewModels
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
