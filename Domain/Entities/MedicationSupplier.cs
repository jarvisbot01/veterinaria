namespace Domain.Entities
{
    public class MedicationSupplier : BaseEntity
    {
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
