using Service_1.Infrastructure;

namespace Service_1.APIs;

public class PublishersService : PublishersServiceBase
{
    public PublishersService(Service_1DbContext context)
        : base(context) { }
}
