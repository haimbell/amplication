using Service_1.Infrastructure;

namespace Service_1.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(Service_1DbContext context)
        : base(context) { }
}
