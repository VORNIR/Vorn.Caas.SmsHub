using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
public static class Extensions
{
    public static void AddSmsHubClient(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.AddEntityHubClient<Sms>();
        webApplicationBuilder.Services.Configure<SmsHubConfiguration>(webApplicationBuilder.Configuration.GetSection(SmsHubConfiguration.Section));
        webApplicationBuilder.Services.AddScoped<ISmsHub, SmsHubClient>();
    }
    public static void MapSmsHub(this WebApplication app)
    {
        app.MapHub<SmsHubServer>(app.Configuration.GetSection(SmsHubConfiguration.Section).Get<SmsHubConfiguration>().Endpoint);
    }
}
