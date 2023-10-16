namespace Domain.Entities
{
    public class Laboratory : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<Medication> Medications { get; set; }
    }
}
