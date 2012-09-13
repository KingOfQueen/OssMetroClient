using ossClient.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ossClient
{
    [Export(typeof(ILeftView))]
    public class NavigateViewModel : ILeftView
    {

    }
}
