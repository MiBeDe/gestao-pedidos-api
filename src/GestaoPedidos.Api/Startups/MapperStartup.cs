using GestaoPedidos.Application.Profiles;

namespace GestaoPedidos.Api.Startups
{
    public static class MapperStartup
    {
        public static void AddCustomizedMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
        }
    }
}
