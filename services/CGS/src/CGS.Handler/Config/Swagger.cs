namespace CGS.Config
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
