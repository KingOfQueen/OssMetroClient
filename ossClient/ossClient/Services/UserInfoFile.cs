using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Services
{
    class UserInfoFile
    {
        public static DirectoryInfo fileDir = InfoDir.getClientDir();
        public static string filename = fileDir.ToString() + @"/userInfo.dat";

        public static User readFile()
        {
            User user = new User();
            try
            {
                if (File.Exists(filename))
                {
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();
                    user = (User)bf.Deserialize(fs);
                    fs.Close();
                }
            }
            catch (Exception e)
            {

            }
            return user;
        }

        public static bool saveFile(User user)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);  //创建一个文件流对象  
                BinaryFormatter bf = new BinaryFormatter();  //创建一个序列化和反序列化对象  
                bf.Serialize(fs, user);   //要先将User类先设为可以序列化(即在类的前面加[Serializable])。将用户集合信息写入到硬盘中  
                fs.Close();   //关闭文件流  
            }
            catch (Exception e)
            {

                return false;

            }
            return true;
        }



    }
}
