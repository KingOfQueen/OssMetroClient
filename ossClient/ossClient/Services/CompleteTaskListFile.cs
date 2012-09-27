using OssClientMetro.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Services
{
    class CompleteTaskListFile
    {
        public static DirectoryInfo fileDir = InfoDir.getClientDir();
        public static string fileName = fileDir.ToString() + @"/completeObject.dat";


        static public List<ObjectModel> readFromFile()
        {
            if (File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length != 0)
                    {
                        BinaryFormatter bin = new BinaryFormatter();

                        return (List<ObjectModel>)bin.Deserialize(fs);
                    }
                    else
                        return null;
                }
            }
            else
                return null;
        }

        static public void writeToFile(List<ObjectModel> data)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(fs, data);
            }
        }

    }
}
