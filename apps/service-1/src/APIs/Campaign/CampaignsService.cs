using Service_1.Infrastructure;

namespace Service_1.APIs;

public class CampaignsService : CampaignsServiceBase
{
    public CampaignsService(Service_1DbContext context)
        : base(context) { }
}
