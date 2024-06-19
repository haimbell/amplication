using Microsoft.AspNetCore.Mvc;

namespace Service_1.APIs;

[ApiController()]
public class PublishersController : PublishersControllerBase
{
    public PublishersController(IPublishersService service)
        : base(service) { }
}
