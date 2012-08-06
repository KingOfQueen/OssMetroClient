//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using Aliyun.OpenServices.OpenStorageService;
//using System.IO;
//using MahApps.Metro.Controls;

//namespace ossClient
//{
//    /// <summary>
//    /// Interaction logic for MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : MetroWindow
//    {
//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void button1_Click(object sender, RoutedEventArgs e)
//        {

//            OssClient client = new OssClient();

//            //OssClient client = new OssClient(id.Text, key.Text);

//            ObjectMetadata metadata = new ObjectMetadata();
//              metadata.UserMetadata.Add("myfield", "test");

//             string key = "sampleobject";

//            IEnumerable<Bucket> bucketList =  client.ListBuckets();
            
//            string text = "I am from .net client";
//            string bucketName = bucketList.First().Name;

//            //using (FileStream fStream = new FileStream("1.txt", FileMode.Open, FileAccess.Read))
//            //{
//            //    client.PutObject(bucketName, key, fStream, metadata);
//            //}

//           // client.DeleteObject(bucketName, key);
//            client.CreateBucket("mydoc");


//        }
//    }
//}
