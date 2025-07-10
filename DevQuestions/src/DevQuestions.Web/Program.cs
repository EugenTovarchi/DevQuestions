using Microsoft.OpenApi.Models;

namespace DevQuestions.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevQuestions", Version = "v1" });
        });

        var app = builder.Build();

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
