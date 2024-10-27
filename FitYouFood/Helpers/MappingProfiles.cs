using AutoMapper;
using FitYouFood.API.Dtos.Exercise;
using FitYouFood.API.Dtos.ExerciseData;
using FitYouFood.Core.Entities;

namespace FitYouFood.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseDto, Exercise>();

            CreateMap<ExerciseData, ExerciseDataDto>();
            CreateMap<ExerciseDataDto, ExerciseData>();
            CreateMap<ExerciseData, ExerciseDataCreateDto>();
            CreateMap<ExerciseDataCreateDto, ExerciseData>();
            CreateMap<ExerciseData, ExerciseDataUpdateDto>();
            CreateMap<ExerciseDataUpdateDto, ExerciseData>();
        }
    }
}
