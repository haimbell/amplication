using Microsoft.AspNetCore.Mvc;

namespace Service_1.APIs;

[ApiController()]
public class CampaignsController : CampaignsControllerBase
{
    public CampaignsController(ICampaignsService service)
        : base(service) { }
}
