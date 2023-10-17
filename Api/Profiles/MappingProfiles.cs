using Api.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class MappingProfles : Profile
    {
        public MappingProfles()
        {
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Pet, PetDto>().ReverseMap();
            CreateMap<Species, SpeciesDto>().ReverseMap();
            CreateMap<Breed, BreedDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Laboratory, LaboratoryDto>().ReverseMap();
            CreateMap<MedicalTreatment, MedicalTreatmentDto>().ReverseMap();
            CreateMap<Medication, MedicationDto>().ReverseMap();
            CreateMap<MedicationMovement, MedicationMovementDto>().ReverseMap();
            CreateMap<MedicationSupplier, MedicationSupplierDto>().ReverseMap();
            CreateMap<MovementDetail, MovementDetailDto>().ReverseMap();
            CreateMap<MovementType, MovementTypeDto>().ReverseMap();
            CreateMap<Rol, RolDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Vet, VetDto>().ReverseMap();
        }
    }
}
