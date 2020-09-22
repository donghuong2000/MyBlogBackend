﻿using Microsoft.EntityFrameworkCore;
using MyBlog.Model;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Data.Data
{
    public class ApplicationDB : DbContext
    {
        public ApplicationDB( DbContextOptions options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        

        public DbSet<User> Users { get; set; }
    }
}
