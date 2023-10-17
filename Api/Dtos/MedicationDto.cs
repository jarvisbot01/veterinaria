namespace Api.Dtos
{
    public class MedicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int LaboratoryId { get; set; }
    }
}
