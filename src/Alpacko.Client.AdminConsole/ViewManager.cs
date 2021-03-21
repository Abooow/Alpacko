using Alpacko.Client.AdminConsole.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alpacko.Client.AdminConsole
{
    public static class ViewManager
    {
        private static List<Service> services = new List<Service>();

        public static T AddService<T>()
        {
            Type type = typeof(T);
            ConstructorInfo constructor = type.GetConstructors()[0];

            List<object> parameters = new List<object>();
            foreach (ParameterInfo param in constructor.GetParameters())
            {
                parameters.Add(FindServiceOfType(param.ParameterType).Instance);
            }

            object instance = constructor.Invoke(parameters.ToArray());

            Service service = new Service
            {
                Type = typeof(T),
                Instance = instance
            };

            services.Add(service);
            return (T)instance;
        }

        public static T AddService<T>(T instance)
        {
            Service service = new Service
            {
                Type = typeof(T),
                Instance = instance
            };

            services.Add(service);
            return instance;
        }

        public static T GetService<T>()
        {
            return (T)FindServiceOfType(typeof(T)).Instance;
        }

        public static void RenderView<T>() where T : IView
        {
            IView view = GetView<T>();
            view.Render();
        }

        public static T GetView<T>() where T : IView
        {
            Type type = typeof(T);
            ConstructorInfo constructor = type.GetConstructors()[0];

            List<object> parameters = new List<object>();
            foreach (ParameterInfo param in constructor.GetParameters())
            {
                parameters.Add(FindServiceOfType(param.ParameterType).Instance);
            }

            T instance = (T)constructor.Invoke(parameters.ToArray());
            return instance;
        }

        public static void Dispose()
        {
            foreach (Service service in services)
            {
                if (service.Type.IsAssignableFrom(typeof(IDisposable)))
                    ((IDisposable)service.Instance).Dispose();
            }
        }

        private static Service FindServiceOfType(Type type)
        {
            return services.First(x => x.Type == type);
        }

        private class Service
        {
            public Type Type { get; set; }
            public object Instance { get; set; }
        }
    }
}