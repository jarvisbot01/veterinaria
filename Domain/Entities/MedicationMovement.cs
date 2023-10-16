namespace Domain.Entities
{
    public class MedicationMovement : BaseEntity
    {
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        public int MovementTypeId { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public ICollection<MovementDetail> MovementDetails { get; set; } =
            new HashSet<MovementDetail>();
    }
}
