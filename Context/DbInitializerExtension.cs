namespace IdealDiscuss.Context
{
    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<IdealDiscussContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
               
            }

            return app;
        }
    }
}
