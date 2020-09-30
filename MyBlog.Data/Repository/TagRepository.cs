using MyBlog.Data.Data;
using MyBlog.Data.Repository.IRepository;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Data.Repository
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly ApplicationDB _db;

        public TagRepository(ApplicationDB db) : base(db)
        {
            _db = db;
        }
        public void Update(Tag tag)
        {
            var obj = _db.Tags.FirstOrDefault(o => o.Id == tag.Id);
            if (obj != null)
            {
                obj.Name = tag.Name;
            }

        }
    }
}
