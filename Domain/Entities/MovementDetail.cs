namespace Domain.Entities
{
    public class MovementDetail : BaseEntity
    {
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        public int MedicationMovementId { get; set; }
        public MedicationMovement MedicationMovement { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
