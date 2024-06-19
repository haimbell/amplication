using Microsoft.AspNetCore.Mvc;

namespace Service_1.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
