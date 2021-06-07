using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Infrastructure
{
    public static class InfraServices
    {
        public static IServiceCollection ConfigureInfra(this IServiceCollection services)
        {
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
            return services;
        }
    }
}
