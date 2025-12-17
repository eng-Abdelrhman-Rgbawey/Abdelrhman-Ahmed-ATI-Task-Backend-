using Api.DTOs;
using Api.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateStudentDto, Student>();

            CreateMap<Course, CourseDto>()
              .ForMember(dest => dest.StudentIds, opt => opt.MapFrom(src =>
                  src.StudentCourses.Select(sc => sc.StudentId).ToList()));

            CreateMap<Course, CourseStatisticsDto>()
               .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.StudentCourses.Count))
               .ForMember(dest => dest.Percentage, opt => opt.Ignore());

            CreateMap<Student, StudentDto>()
                       
                       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
                       .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                       .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                       .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                       .ForMember(dest => dest.CourseCount, opt => opt.MapFrom(src => src.StudentCourses.Count()));
        }
    }
}
