using ossClient.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ossClient
{
     [Export(typeof(IRightView))]
    class ContentViewModel : IRightView
    {
    }
}
