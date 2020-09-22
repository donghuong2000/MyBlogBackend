using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBlog.Model
{
    public class Post
    {
        public int ID { get; set; }

        public string Title { get; set; }
        public DateTime DateCreate { get; set; }
        public string Content { get; set; }

        public int Like { get; set; }

        public int Share { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public string Tags { get; set; }





    }
}
