namespace Domain.Entities
{
    public class Medication : BaseEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int LaboratoryId { get; set; }
        public Laboratory Laboratory { get; set; }

        public ICollection<MedicalTreatment> MedicalTreatments { get; set; }
        public ICollection<MovementDetail> MovementDetails { get; set; }
        public ICollection<MedicationSupplier> MedicationSuppliers { get; set; }
    }
}
