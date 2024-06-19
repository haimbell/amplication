using Service_1.Infrastructure;

namespace Service_1.APIs;

public class ConversionsService : ConversionsServiceBase
{
    public ConversionsService(Service_1DbContext context)
        : base(context) { }
}
