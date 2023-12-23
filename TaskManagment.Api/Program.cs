using Autofac.Core;
using Autofac;
using System.IdentityModel.Tokens.Jwt;
using TaskManagment.Aplicacion.Infrastructure.AutofacModules;
using TaskManagment.Api.Infraestructura;
using Autofac.Extensions.DependencyInjection;
using TaskManagment.Infrastructure.Services.Token;
using TaskManagment.Shared;

namespace TaskManagment.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOptions();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddCustomMvc(builder.Configuration)
                .AddCustomIntegrations(builder.Configuration)
                .AddCustomConfiguration(builder.Configuration)
                .AddCustomSwagger(builder.Configuration)
                .AddCustomAuth(builder.Configuration);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule(new MediatorModule());
                container.RegisterModule(new ApplicationModule(builder.Configuration["AppSettings:Token:Bearer"], builder.Configuration["AppSettings:Token:Expires"]));
            });

            var app = builder.Build();
            var version = new VersionUtil().GetAssemblyVersion();

            app.UseForwardedHeaders();
            app.UseCors("CorsPolicy");

           
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Warehouse.API {version}");
            });
            

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }
    }

}