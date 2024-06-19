using Service_1.Infrastructure;

namespace Service_1.APIs;

public class ClicksService : ClicksServiceBase
{
    public ClicksService(Service_1DbContext context)
        : base(context) { }
}
