using Microsoft.OpenApi.Models;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevQuestions", Version = "v1" });
        });

        builder.Services.AddProgramDependencies();

        var app = builder.Build();

        //app.UseExceptionMiddleware();

        if (app.Environment.IsDevelopment())
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Remove("Content-Encoding");
                await next();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevQuestions v1"); });
        }

        app.MapControllers();

        app.Run();
    }
}
