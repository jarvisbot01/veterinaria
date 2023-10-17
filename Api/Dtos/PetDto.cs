namespace Api.Dtos
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public int BreedId { get; set; }
        public int SpeciesId { get; set; }
        public int OwnerId { get; set; }
    }
}
