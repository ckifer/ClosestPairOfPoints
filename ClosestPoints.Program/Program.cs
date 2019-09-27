using Autofac;
using ClosestPoints.Models;
using ClosestPoints.Program.Config;
using System;
using System.Net;

namespace ClosestPoints.Program
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                IContainer container = ContainerConfig.Configure();

                // dependency injection scope
                using (var scope = container.BeginLifetimeScope())
                {
                    // begin application
                    scope.Resolve<IApplication>().Run();
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
