namespace ProyectoUniversidad.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigurarCORS(this IServiceCollection services, string MyAllowSpecifiOrigins)
        {
            string[] domains = { "https://productionweb-production.up.railway.app", "http://localhost:4200" };


            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecifiOrigins,
                    policy =>
                    {
                        policy.WithOrigins(domains)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }

    }
}
