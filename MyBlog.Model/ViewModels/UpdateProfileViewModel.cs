using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyBlog.Models.ViewModels
{
    public class UpdateProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public IFormFile AvatarUrl { get; set; }

    }
}
