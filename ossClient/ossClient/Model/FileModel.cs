using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OssClientMetro.Model
{
    public class FileModel : ObjectModel
    {

        public DateTime modifyTime { get; set; }

        void callback(HttpProcessData httpProcessData)
        {

       //     TaskFactory factory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
         
       //     factory.StartNew( () =>
 
       // {
       //     Percent = state.ProgressPercentage;
       //     Percent = httpProcessData.ProgressPercentage;
       //     Percent = httpProcessData.ProgressPercentage;


       // }, httpProcessData);

            
       //     Task<HttpProcessData> taskWithFactoryAndState =
       //   Task.Factory.StartNew<HttpProcessData>(() =>
       //      {
       //          return processPercent;
       //      });

       //taskWithFactoryAndState.ContinueWith((ant) =>
 
       // {
       //     Percent = ant.Result.ProgressPercentage;
       //     Percent = ant.Result.ProgressPercentage;
       //     Percent = ant.Result.ProgressPercentage;


       // }, processPercent, SynchronizationContext.Current);
 




        }

    }
}
