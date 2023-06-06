using Autofac;
using Business.DependencyResolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestBackEnd.AutofacContainerBuild
{
    public class AutofacContainerBuilder
    {
        public static IContainer AutofacContainer { get; private set; }

        public static IContainer GetAutofacContainer()
        {
            var containerBuilder=new ContainerBuilder();
            containerBuilder.RegisterModule(new AutofacBusinessModule());
            AutofacContainer= containerBuilder.Build();
            return AutofacContainer;
        }
    }
}
