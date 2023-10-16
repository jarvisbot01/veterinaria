namespace Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<MedicationSupplier> MedicationSuppliers { get; set; }
    }
}
