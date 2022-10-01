namespace CGS.Config
{
    public static class HttpConfig
    {
        public static void AddHttp(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
