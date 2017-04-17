using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public string UserId { get; set; }


        // Navigation Properties.

        public Article Article { get; set; }

        public User User { get; set; }
    }
}
