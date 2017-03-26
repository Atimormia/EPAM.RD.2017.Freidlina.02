using System.Collections.Generic;
using PhotoGallery.DB.Infrastructure.Core;
using PhotoGallery.DB.Model;

namespace PhotoGallery.DB.Infrastructure.Services.Abstract
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int[] roles);
        User GetUser(int userId);
        List<Role> GetUserRoles(string username);
    }
}