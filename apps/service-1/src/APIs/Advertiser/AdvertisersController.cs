using Microsoft.AspNetCore.Mvc;

namespace Service_1.APIs;

[ApiController()]
public class AdvertisersController : AdvertisersControllerBase
{
    public AdvertisersController(IAdvertisersService service)
        : base(service) { }
}
