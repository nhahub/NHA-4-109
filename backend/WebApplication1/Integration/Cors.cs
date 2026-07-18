namespace PresentationLayer.Integration
{
    public static class Cors
    {
        public const string ReactAppPolicy = "ReactAppPolicy";

        public static IServiceCollection AddReactCors(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var allowedOrigins = configuration["Cors:ReactAppOrigin"]
                                 ?? throw new InvalidOperationException("Cors:ReactAppOrigin is not set");

            services.AddCors(options =>
            {
                options.AddPolicy(ReactAppPolicy, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }
    }
}