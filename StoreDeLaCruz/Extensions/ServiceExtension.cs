using Asp.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace StoreDeLaCruz.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwagerExtension(this IServiceCollection service)
        {
            //Configuracion para UI
            service.AddSwaggerGen(option =>
            {

                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Store De La Cruz",
                    Description = "Esta API de una tienda",
                    Contact = new OpenApiContact
                    {
                        Name = "Wilmer De La Cruz",
                        Email = "WilmerDeLaCruz@gmail.com"
                    }

                });

                option.DescribeAllParametersInCamelCase();

            });
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true; //Cuando no se mande versiones, con esto se asume la version por default que es V1
                options.ReportApiVersions = true;
            });
        }

    }
}
