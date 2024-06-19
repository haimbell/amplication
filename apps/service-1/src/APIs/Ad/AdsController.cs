using Microsoft.AspNetCore.Mvc;

namespace Service_1.APIs;

[ApiController()]
public class AdsController : AdsControllerBase
{
    public AdsController(IAdsService service)
        : base(service) { }
}
