using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiVersioControl
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Api .NET restful",
                Version = description.ApiVersion.ToString(),
                Description = "API version control.",
                Contact = new OpenApiContact()
                {
                    Email = "rl@gmail.com",
                    Name = "Rui Lagos",
                }
            };
            if (description.IsDeprecated)
            {
                info.Description += "This version has been deprecated";
            }
            return info;

        }

        public void Configure(SwaggerGenOptions options)
        {
            // Add Swagger Documentation for each API version
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }
        
        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        
    }
}
