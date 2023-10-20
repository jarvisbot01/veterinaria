using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISupplier : IGeneric<Supplier>
    {
        Task<object> Consulta4B();
    }
}
