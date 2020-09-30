using MyBlog.Data.Data;
using MyBlog.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDB _db;

        public UnitOfWork(ApplicationDB db)
        {
            _db = db;
            SP_Call = new SP_Call(_db);
            Tag = new TagRepository(_db);
            Post = new PostRepository(_db);
        }


        public ISP_Call SP_Call { get; private set; }

        public ITagRepository Tag { get; private set; }

        

        public IPostRepository Post { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
