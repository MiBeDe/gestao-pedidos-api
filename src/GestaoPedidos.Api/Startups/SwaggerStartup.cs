using Microsoft.OpenApi;
using System.Reflection;

namespace GestaoPedidos.Api.Startups
{
    public static class SwaggerStartup
    {
        public static void AddCustomizedSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "GestaoPedidos", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseCustomizedSwagger(this WebApplication app)
        {
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/openapi/v1.json", "API Gestao Pedidos");

            });
        }
    }
}
