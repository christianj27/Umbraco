using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.Models
{
    public class BlogPreview
    {
        public string Name { get; set; }
        public string Instroduction { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }
        public string Creator { get; set; }
        public string PublishedDate { get; set; }

        public BlogPreview(string name, string instroduction, string imageUrl, string linkUrl, string creator, string publishedDate)
        {
            Name = name;
            Instroduction = instroduction;
            ImageUrl = imageUrl;
            LinkUrl = linkUrl;
            Creator = creator;
            PublishedDate = publishedDate;
        }
    }
}