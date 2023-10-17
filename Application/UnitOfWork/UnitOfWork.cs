using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PetShopContext _context;
        private IAppointment _appointments;
        private IBreed _breeds;
        private ILaboratory _laboratories;
        private IMedicalTreatment _medicalTreatments;
        private IMedicationMovement _medicationMovements;
        private IMedication _medications;
        private IMedicationSupplier _medicationSuppliers;
        private IMovementDetail _movementDetails;
        private IMovementType _movementTypes;
        private IOwner _owners;
        private IPet _pets;
        private IRol _roles;
        private ISpecies _species;
        private ISupplier _suppliers;
        private IUser _users;
        private IVet _vets;

        public UnitOfWork(PetShopContext context)
        {
            _context = context;
        }

        public IAppointment Appointments
        {
            get
            {
                if (_appointments == null)
                {
                    _appointments = new AppointmentRepository(_context);
                }
                return _appointments;
            }
        }

        public IBreed Breeds
        {
            get
            {
                if (_breeds == null)
                {
                    _breeds = new BreedRepository(_context);
                }
                return _breeds;
            }
        }

        public ILaboratory Laboratories
        {
            get
            {
                if (_laboratories == null)
                {
                    _laboratories = new LaboratoryRepository(_context);
                }
                return _laboratories;
            }
        }

        public IMedicalTreatment MedicalTreatments
        {
            get
            {
                if (_medicalTreatments == null)
                {
                    _medicalTreatments = new MedicalTreatmentRepository(_context);
                }
                return _medicalTreatments;
            }
        }

        public IMedicationMovement MedicationMovements
        {
            get
            {
                if (_medicationMovements == null)
                {
                    _medicationMovements = new MedicationMovementRepository(_context);
                }
                return _medicationMovements;
            }
        }

        public IMedication Medications
        {
            get
            {
                if (_medications == null)
                {
                    _medications = new MedicationRepository(_context);
                }
                return _medications;
            }
        }

        public IMedicationSupplier MedicationSuppliers
        {
            get
            {
                if (_medicationSuppliers == null)
                {
                    _medicationSuppliers = new MedicationSupplierRepository(_context);
                }
                return _medicationSuppliers;
            }
        }

        public IMovementDetail MovementDetails
        {
            get
            {
                if (_movementDetails == null)
                {
                    _movementDetails = new MovementDetailRepository(_context);
                }
                return _movementDetails;
            }
        }

        public IMovementType MovementTypes
        {
            get
            {
                if (_movementTypes == null)
                {
                    _movementTypes = new MovementTypeRepository(_context);
                }
                return _movementTypes;
            }
        }

        public IOwner Owners
        {
            get
            {
                if (_owners == null)
                {
                    _owners = new OwnerRepository(_context);
                }
                return _owners;
            }
        }

        public IPet Pets
        {
            get
            {
                if (_pets == null)
                {
                    _pets = new PetRepository(_context);
                }
                return _pets;
            }
        }

        public IRol Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolRepository(_context);
                }
                return _roles;
            }
        }

        public ISpecies Species
        {
            get
            {
                if (_species == null)
                {
                    _species = new SpeciesRepository(_context);
                }
                return _species;
            }
        }

        public ISupplier Suppliers
        {
            get
            {
                if (_suppliers == null)
                {
                    _suppliers = new SupplierRepository(_context);
                }
                return _suppliers;
            }
        }

        public IUser Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }

        public IVet Vets
        {
            get
            {
                if (_vets == null)
                {
                    _vets = new VetRepository(_context);
                }
                return _vets;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
