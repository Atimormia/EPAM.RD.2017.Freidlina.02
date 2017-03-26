using PhotoGallery.DB.Infrastructure.Repositories.Abstract;
using PhotoGallery.DB.Model;

namespace PhotoGallery.DB.Infrastructure.Repositories
{
    public class LoggingRepository : EntityBaseRepository<Error>, ILoggingRepository
    {
        public LoggingRepository(PhotosDB context)
            : base(context)
        { }

        public override void Commit()
        {
            try
            {
                base.Commit();
            }
            catch { }
        }
    }
}