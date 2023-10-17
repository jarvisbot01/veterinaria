namespace Api.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public int PetId { get; set; }
        public int VetId { get; set; }
    }
}
