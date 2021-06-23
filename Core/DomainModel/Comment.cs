using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}")]
        public DateTime Date 
        {
            get
            {
                return _date ?? DateTime.Now;
            }
            set
            {
                _date = value;
            }
        }

        [Required]
        public string Author { get; set; }

        public int AnimalID { get; set; }

        [ForeignKey("AnimalID")]
        public Animal CommentedOn { get; set; }

        private DateTime? _date;
    }
}