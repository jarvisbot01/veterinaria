namespace Domain.Entities
{
    public class Vet : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialty { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
