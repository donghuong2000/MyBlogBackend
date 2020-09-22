using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string AvatarUrl { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }


    }
}
