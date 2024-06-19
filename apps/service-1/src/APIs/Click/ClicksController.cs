using Microsoft.AspNetCore.Mvc;

namespace Service_1.APIs;

[ApiController()]
public class ClicksController : ClicksControllerBase
{
    public ClicksController(IClicksService service)
        : base(service) { }
}
