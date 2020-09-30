using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Data.Repository.IRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        void Update(Post post);
    }
}
