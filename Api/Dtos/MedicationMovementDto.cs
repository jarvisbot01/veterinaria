namespace Api.Dtos
{
    public class MedicationMovementDto
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public int MovementTypeId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
