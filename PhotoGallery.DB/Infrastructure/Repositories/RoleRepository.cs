using PhotoGallery.DB.Infrastructure.Repositories.Abstract;
using PhotoGallery.DB.Model;

namespace PhotoGallery.DB.Infrastructure.Repositories
{
    public class RoleRepository : EntityBaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(PhotosDB context)
            : base(context)
        { }
    }
}