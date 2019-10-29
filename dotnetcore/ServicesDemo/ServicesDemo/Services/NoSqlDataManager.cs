using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesDemo.Services
{
    public class NoSqlDataManager:IDataManager
    {
        public string GetData()
        {
            return "NoSqlDataManager - Hello World1!";
        }
        public string GetGreetings(string name)
        {
            return $"NoSqlDataManger - Good morning {name}";
        }
    }
}
