using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesDemo.Services
{
    public interface IDataManager
    {
        string GetData();
        string GetGreetings(string s);
        
    }
}
