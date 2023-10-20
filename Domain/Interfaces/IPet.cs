using Domain.Entities;

namespace Domain.Interfaces
{

    public interface IPet : IGeneric<Pet>
    {
        Task<IEnumerable<Pet>> Consulta1B();
        Task<object> Consulta3B();
        Task<object> Consulta6B();
    }
}
