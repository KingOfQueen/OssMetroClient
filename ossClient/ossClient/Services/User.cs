using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Services
{
    [Serializable]  //表示这个类可以被序列化  
    public class User   //用户类  
    {
        string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        string _userPassword;
        public string UserPassword
        {
            get
            {
                if (_userPassword != "")  //如果密码不为空  
                    return MemoryPassword.DecryptDES(_userPassword); //将密码进行解密后再返出去  
                return _userPassword;
            }
            set { _userPassword = value; }
        }

        public User(string userName, string userPassword)  //用户类的构造函数  
        {
            _userName = userName;
            _userPassword = userPassword;
        }

        public User()  //用户类的构造函数  
        {
            _userName = "";
            _userPassword = "";
        }
    }
}
