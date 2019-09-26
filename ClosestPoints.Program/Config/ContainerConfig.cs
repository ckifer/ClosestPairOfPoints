using Autofac;
using ClosestPoint.Services;
using ClosestPoints.Models;

namespace ClosestPoints.Program.Config
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<ClosestPointService>().AsSelf();

            return builder.Build();
        }
    }
}
