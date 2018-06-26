using Abp.Authorization;
using MyFirstABPProject.Authorization.Roles;
using MyFirstABPProject.Authorization.Users;

namespace MyFirstABPProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
