namespace CGS.Config
{
    public static class CORSConfig
    {
        public static readonly string defaultCorsPolicyName = "AllowAll";
        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(p => p.AddPolicy(defaultCorsPolicyName, builder =>
            {
                builder.AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials()
                                .SetIsOriginAllowed(_ => true);
            }));

        }

        public static void AddCors(this WebApplication app)
        {
            app.UseCors(defaultCorsPolicyName);
        }
    }
}
