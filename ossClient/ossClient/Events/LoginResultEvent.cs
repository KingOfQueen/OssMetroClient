using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    public enum Result
    {
        SUCCESS,
        FAULT
    };


    public class LoginResultEvent
    {
        public LoginResultEvent(Result _result, string _message)
        {
            result = _result;
            message = _message;
        }

        public Result result { set; get; }
        public string message{set;get;}
    }
}
