using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Post> Posts { get; set; }

        public List<Post> BestPosts { get; set; }
    }
}
