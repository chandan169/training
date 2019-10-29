using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesDemo.Services
{
    public class SqlDataManager : IDataManager
    {
        public string GetData()
        {
            return "SqlDataManager - Hello World1!";
        }
        public string GetGreetings(string name)
        {
            return $"SqlDataManger - Good morning {name}";
        }
    }
}
