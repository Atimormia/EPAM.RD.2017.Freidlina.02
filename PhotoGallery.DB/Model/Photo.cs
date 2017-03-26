using System;

namespace PhotoGallery.DB.Model
{
    public class Photo: IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public virtual Album Album { get; set; }
        public int AlbumId { get; set; }
        public DateTime DateUploaded { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}