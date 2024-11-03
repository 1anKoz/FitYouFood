using AutoMapper;
using FitYouFood.API.Dtos.Account;
using FitYouFood.API.Dtos.Exercise;
using FitYouFood.API.Dtos.ExerciseDataDtos;
using FitYouFood.API.Dtos.Ingredient;
using FitYouFood.API.Dtos.Training;
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

            CreateMap<Training, TrainingDto>();
            CreateMap<TrainingDto, Training>();
            CreateMap<Training, TrainingCreateDto>();
            CreateMap<TrainingCreateDto, Training>();
            CreateMap<Training, TrainingUpdateDto>();
            CreateMap<TrainingUpdateDto, Training>();

            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<Ingredient, IngredientDto>();
            CreateMap<IngredientDto, Ingredient>();
        }
    }
}
