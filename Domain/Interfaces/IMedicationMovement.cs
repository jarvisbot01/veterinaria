using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMedicationMovement : IGeneric<MedicationMovement>
    {
        Task<object> Consulta2B();
    }
}
