using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }

        public IEnumerable<int> TagSelected { get; set; }
    }
}
