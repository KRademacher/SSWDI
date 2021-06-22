using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel
{
    public class InterestedAnimal
    {
        [Key]
        [Required]
        public int ID { get; set; }

        public int AnimalID { get; set; }
        public Animal Animal { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}