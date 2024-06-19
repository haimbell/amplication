using Service_1.Infrastructure;

namespace Service_1.APIs;

public class ImpressionsService : ImpressionsServiceBase
{
    public ImpressionsService(Service_1DbContext context)
        : base(context) { }
}
