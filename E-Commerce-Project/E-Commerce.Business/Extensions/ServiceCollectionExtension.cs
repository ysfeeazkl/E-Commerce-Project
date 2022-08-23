using E_Commerce.Business.Abstract;
using E_Commerce.Business.Concrete;
using E_Commerce.Data.Concrete.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection services)
        {
            services.AddDbContext<CommerceContext>(contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Transient);
            services.AddSingleton<IAuthService, AuthManager>();

            return services;

        }
    }
}
