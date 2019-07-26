using Microsoft.AspNetCore.Authorization;
using Studio.Common;

namespace Studio.User.WebApp.Controllers
{
    [Authorize(Roles = GConst.UserRole)]
    public class UserController
    {
        
    }
}