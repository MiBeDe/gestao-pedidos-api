namespace GestaoPedidos.Api.Startups
{
    public static class CorsStartup
    {
        public static void AddCustomizedCors(this IServiceCollection services)
        {
            services.AddCors(x => x.AddPolicy("GestaoPedidosPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();

            }));
        }

        public static void UseCustomizedCors(this IApplicationBuilder app)
        {
            app.UseCors("GestaoPedidosPolicy");
        }
    }
}
