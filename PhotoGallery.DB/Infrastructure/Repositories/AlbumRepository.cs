using PhotoGallery.DB.Infrastructure.Repositories.Abstract;
using PhotoGallery.DB.Model;

namespace PhotoGallery.DB.Infrastructure.Repositories
{
    public class AlbumRepository : EntityBaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(PhotosDB context)
            : base(context)
        { }
    }
}