namespace Domain.Entities
{
    public class MovementType : BaseEntity
    {
        public string Description { get; set; }

        public ICollection<MedicationMovement> MedicationMovements { get; set; } =
            new HashSet<MedicationMovement>();
    }
}
