using System.Text.Json.Serialization;

namespace XpertStore.Api.Configuration;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfig(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

        return builder;
    }
}
