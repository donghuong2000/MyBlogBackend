using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBlog.Model
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        public string Content { get; set; }

        public int?  PostId { get; set; }
        
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        public int? UserID { get; set; }


        [ForeignKey("UserID")]
        public User User { get; set; }

        public IEnumerable<Comment> ReplyComments { get; set; }
    }
}
