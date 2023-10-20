using Domain.Entities;

namespace Domain.Interfaces
{

    public interface IOwner : IGeneric<Owner>
    {
        Task<object> Consulta5B();
    }

}
