using ossClient.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ossClient.ViewModels
{
    [Export(typeof(ILeftWorkSpace))]
    class LoginViewModel : ILeftWorkSpace
    {
    }
}
