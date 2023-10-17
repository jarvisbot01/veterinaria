namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAppointment Appointments { get; }
        IBreed Breeds { get; }
        ILaboratory Laboratories { get; }
        IMedicalTreatment MedicalTreatments { get; }
        IMedication Medications { get; }
        IMedicationMovement MedicationMovements { get; }
        IMedicationSupplier MedicationSuppliers { get; }
        IMovementDetail MovementDetails { get; }
        IMovementType MovementTypes { get; }
        IOwner Owners { get; }
        IPet Pets { get; }
        IRol Roles { get; }
        ISpecies Species { get; }
        ISupplier Suppliers { get; }
        IUser Users { get; }
        IVet Vets { get; }

        Task<int> SaveAsync();
    }
}
