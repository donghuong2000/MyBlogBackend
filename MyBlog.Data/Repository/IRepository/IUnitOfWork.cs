using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        ITagRepository Tag { get; }
        ISP_Call SP_Call { get; }
         IPostRepository Post { get; }


        void Save();
    }
}
