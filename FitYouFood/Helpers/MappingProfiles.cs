using AutoMapper;
using FitYouFood.API.Dtos.Exercise;
using FitYouFood.Core.Entities;

namespace FitYouFood.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseDto, Exercise>();
        }
    }
}
