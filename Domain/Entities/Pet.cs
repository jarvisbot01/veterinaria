namespace Domain.Entities
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; }
        public DateOnly Birthdate { get; set; }
        public int BreedId { get; set; }
        public Breed Breed { get; set; }
        public int SpeciesId { get; set; }
        public Species Species { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
