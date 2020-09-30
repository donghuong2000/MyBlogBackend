using MyBlog.Data.Data;
using MyBlog.Data.Repository.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ApplicationDB _db;

        public PostRepository(ApplicationDB db) : base(db)
        {
            _db = db;
        }
        public void Update(Post Post)
        {
            var obj = _db.Posts.FirstOrDefault(o => o.ID == Post.ID);
            if (obj != null)
            {
                obj.Confirm = Post.Confirm;
                obj.Content = Post.Content;
                obj.Title = Post.Title;
            }

        }
    }
}
