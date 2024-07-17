﻿using API.Models.Blog;

namespace API.Models.ViewModel.Blog
{
    public class BlogHomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
