using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel
{
    public class Comment
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Author { get; set; }

    }
}