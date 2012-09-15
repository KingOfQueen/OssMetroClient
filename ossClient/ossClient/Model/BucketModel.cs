using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OssClientMetro.Model
{
    public class BucketModel
    {
        public BucketModel()
        {

        }

        public BucketModel(string _name)
        {
            name = _name;
        }

        public string name { get; set; }
    }
}
