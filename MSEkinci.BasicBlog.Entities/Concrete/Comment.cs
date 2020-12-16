﻿using MSEkinci.BasicBlog.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace MSEkinci.BasicBlog.Entities.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public List<Comment> SubComments { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
    }
}
