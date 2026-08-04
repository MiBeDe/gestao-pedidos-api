using Asp.Versioning;
using Newtonsoft.Json;
using System.Globalization;

namespace GestaoPedidos.Api.Startups
{
    public static class MvcStartup
    {
        public static void AddCustomizedMvc(this IServiceCollection services)
        {
            services.AddControllers(options => {}).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Culture = CultureInfo.CurrentCulture;
            });

            //Api Versioning
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static void UseCustomizedMvc(this WebApplication app)
        {
            app.MapControllers();
        }
    }
}
