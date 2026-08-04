using GestaoPedidos.Application.EventHandlers;
using GestaoPedidos.Application.Interfaces;
using GestaoPedidos.Application.Services;
using GestaoPedidos.CrossCutting.Properties;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Infrastructure.Contexts;
using GestaoPedidos.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoPedidos.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterNativeInjector(this IServiceCollection services)
        {
            services.AddDbContext<GestaoPedidosContext>(options =>
            {
                options.UseSqlServer(
                    Environment.GetEnvironmentVariable(SharedConnection.ConnectionString),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.CommandTimeout(150);
                    });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<HttpClient>();

            services.AddScoped<IClienteAppService, ClienteAppService>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IPedidoAppService, PedidoAppService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(StatusPedidoAlteradoDomainEventHandler).Assembly,
                    typeof(GestaoPedidosContext).Assembly);
            });
        }
    }
}
