using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using DailyToDoManager.DataAccessLayer;

namespace DailyToDoManager.Dependency
{
    public class Bootstrap
    {
        public static void ConfigureUnityContainer()
        {
            IUnityContainer container=new UnityContainer();
            container.RegisterType<IToDoRepository, ToDoRepository>();
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(container));
        }
    }
}