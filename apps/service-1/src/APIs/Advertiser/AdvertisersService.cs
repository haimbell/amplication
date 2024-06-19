using Service_1.Infrastructure;

namespace Service_1.APIs;

public class AdvertisersService : AdvertisersServiceBase
{
    public AdvertisersService(Service_1DbContext context)
        : base(context) { }
}
