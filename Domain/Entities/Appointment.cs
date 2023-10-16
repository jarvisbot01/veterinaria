namespace Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public ICollection<MedicalTreatment> MedicalTreatments { get; set; } =
            new HashSet<MedicalTreatment>();
    }
}
