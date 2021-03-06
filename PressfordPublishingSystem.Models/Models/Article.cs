﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string AuthorId { get; set; }

        public DateTime DatePublished { get; set; }

        public DateTime DateModified { get; set; }


        // Navigation Properties.

        public User Author { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
