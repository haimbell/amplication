using Microsoft.AspNetCore.Mvc;

namespace Service_1.APIs;

[ApiController()]
public class ConversionsController : ConversionsControllerBase
{
    public ConversionsController(IConversionsService service)
        : base(service) { }
}
