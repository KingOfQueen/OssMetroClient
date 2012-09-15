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
     [Export(typeof(IRightView))]
    class RightViewModel : Conductor<IRightWorkSpace>.Collection.OneActive, IRightView
    {
        [ImportingConstructor]
         public RightViewModel([ImportMany]IEnumerable<IRightWorkSpace> workspaces)
         {
             Items.AddRange(workspaces);
             ActivateItem(workspaces.First());
         }
    }


}
