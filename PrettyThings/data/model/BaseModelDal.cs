using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;

namespace PrettyThings.data.model
{
    public abstract class BaseModelDal<T>
    {
        public abstract void InsertOrReplace();
        
        public static T[] ParseFromJsonPath(string path)
        {
            return JsonConvert.DeserializeObject<T[]>(new StreamReader(path).ReadToEnd());
        }
    }
}
