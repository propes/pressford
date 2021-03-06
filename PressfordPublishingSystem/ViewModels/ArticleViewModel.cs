﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }

        public DateTime DatePublished { get; set; }

        public DateTime DateModified { get; set; }
    }
}
