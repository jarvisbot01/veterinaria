namespace Api.Dtos
{
    public class MovementDetailDto
    {
        public int Id { get; set; }
        public int MedicationMovementId { get; set; }
        public int MedicationId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
