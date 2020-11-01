using MyBlog.Data.Data;
using MyBlog.Data.Repository.IRepository;
using MyBlog.Model;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDB _db;

        public UserRepository(ApplicationDB db) : base(db)
        {
            _db = db;
        }
        public void Update(User user)
        {
            var obj = _db.Users.FirstOrDefault(o => o.Id == user.Id);
            if (obj != null)
            {
                obj.UserName = user.UserName;
                obj.Password = user.Password;
                obj.PhoneNumber = user.PhoneNumber;
                obj.Email = user.Email;
                obj.Name = user.Name;
                obj.AvatarUrl = user.AvatarUrl;
                
            }
           

        }
    }
}
