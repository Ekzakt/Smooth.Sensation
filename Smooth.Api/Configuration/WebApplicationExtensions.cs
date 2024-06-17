namespace Smooth.Api.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplication UseResponseSizeCompression(this WebApplication application)
    {
        if (!application.Environment.IsDevelopment())
        {
            application.UseResponseCompression(); ;
        }

        return application;
    }


    public static WebApplication UseSwaggerGen(this WebApplication application) 
    { 
        if (!application.Environment.IsProduction())
        {
            application.UseSwagger();
            application.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        return application;
    }
}
