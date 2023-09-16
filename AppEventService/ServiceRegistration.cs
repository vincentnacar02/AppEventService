using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEventService
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddAppEventService<T>(this IServiceCollection services) where T : IAppEvent
        {
            services.AddSingleton<IAppEventService<T>, AppEventService<T>>();
            return services;
        }
    }
}
