using Microsoft.AspNetCore.Mvc;

namespace Service_1.APIs;

[ApiController()]
public class ImpressionsController : ImpressionsControllerBase
{
    public ImpressionsController(IImpressionsService service)
        : base(service) { }
}
