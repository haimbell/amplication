using Service_1.Infrastructure;

namespace Service_1.APIs;

public class AdsService : AdsServiceBase
{
    public AdsService(Service_1DbContext context)
        : base(context) { }
}
