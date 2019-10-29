using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServicesDemo.Services;
using Autofac;

namespace ServicesDemo
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlDataManager>().As<IDataManager>().SingleInstance();
        }
    }
}
