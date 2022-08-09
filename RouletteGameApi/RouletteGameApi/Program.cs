using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using RouletteGameApi.Extensions;
using RouletteGameApi.Presentation;
using RouletteGameApi.Extensions.ServiceExtensions;
using Contracts;

namespace RouletteGameApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            
            builder.Services.ConfigureCors(); 
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers(config => { 
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters()
               .AddCustomCSVFormatter()
              .AddApplicationPart(typeof(RouletteGameApi.Presentation.AssemblyReference).Assembly); ;

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);

            if (app.Environment.IsProduction())
                app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles(); 
            app.UseForwardedHeaders(new ForwardedHeadersOptions 
            {
                ForwardedHeaders = ForwardedHeaders.All 
            });

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}