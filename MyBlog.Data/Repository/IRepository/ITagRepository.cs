using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Data.Repository.IRepository
{
    public interface ITagRepository : IRepository<Tag>
    {
        void Update(Tag tag);
    }
}
