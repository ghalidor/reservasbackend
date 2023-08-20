
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aplication
{
    public static class DependencyInyection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //services.AddSingleton<DapperContext>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}