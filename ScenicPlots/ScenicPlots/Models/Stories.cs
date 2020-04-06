using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenicPlots.Models
{
    public class Stories
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Story { get; set; }
        public string Dates { get; set; }
        public string StoryHeading { get; set; }
        public string LeadCharacter { get; set; }
        public string Likes { get; set; }
        public string Author { get; set; }
        public string Avatar { get; set; }

    }
}
