using System.Security.Principal;
using PhotoGallery.DB.Model;

namespace PhotoGallery.DB.Infrastructure.Core
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public User User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}