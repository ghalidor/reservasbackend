using Aplication.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services) {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IMesasRepository, MesasRepository>();
            services.AddScoped<IZonasRepository, ZonasRepository>();
            services.AddScoped<IReservasRepository,ReservasRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IReservaMesaRepository, ReservaMesaRepository>();
        }
    }
}
