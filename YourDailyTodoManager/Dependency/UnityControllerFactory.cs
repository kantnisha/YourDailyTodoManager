namespace DailyToDoManager.Dependency
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Practices.Unity;

    public class UnityControllerFactory : DefaultControllerFactory
    {
        IUnityContainer container;

        public UnityControllerFactory(IUnityContainer unityContainer)
        {
            this.container = unityContainer;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                {
                    throw new ArgumentNullException("controllerType");
                }

                if (!typeof(IController).IsAssignableFrom(controllerType))
                {
                    throw new ArgumentException("Type requested is not a controller");
                }

                return container.Resolve(controllerType) as IController;
            }
            catch
            {
                return null;
            }
        }
    }
}