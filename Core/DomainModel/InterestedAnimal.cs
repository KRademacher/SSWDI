namespace Core.DomainModel
{
    public class InterestedAnimal
    {
        public int AnimalID { get; set; }
        public Animal Animal { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}