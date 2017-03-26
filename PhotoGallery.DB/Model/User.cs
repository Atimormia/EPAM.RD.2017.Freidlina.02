using System;
using System.Collections.Generic;

namespace PhotoGallery.DB.Model
{
    public class User:IEntity
    {
        public User()
        {
            UserRoles = new List<UserRole>();
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}