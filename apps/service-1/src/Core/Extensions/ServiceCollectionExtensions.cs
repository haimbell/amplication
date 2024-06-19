using Service_1.APIs;

namespace Service_1;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAdsService, AdsService>();
        services.AddScoped<IAdvertisersService, AdvertisersService>();
        services.AddScoped<ICampaignsService, CampaignsService>();
        services.AddScoped<IClicksService, ClicksService>();
        services.AddScoped<IConversionsService, ConversionsService>();
        services.AddScoped<IImpressionsService, ImpressionsService>();
        services.AddScoped<IPublishersService, PublishersService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
