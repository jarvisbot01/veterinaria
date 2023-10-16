namespace Domain.Entities
{
    public class Species : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Pet> Pets { get; set; }
        public ICollection<Breed> Breeds { get; set; }
    }
}
