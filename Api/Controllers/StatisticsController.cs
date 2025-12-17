using Api.DTOs;
using Api.Models;
using Api.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StatisticsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourseStatistics()
        {
            var courses = await _unitOfWork.Courses.GetAll();
            var totalStudents = (await _unitOfWork.Students.GetAll()).Count();

            var courseDtos = _mapper.Map<List<CourseStatisticsDto>>(courses);

            foreach (var dto in courseDtos)
            {
                dto.Percentage = totalStudents == 0
                    ? 0
                    : (double)dto.StudentCount / totalStudents * 100;
            }

            return Ok(courseDtos);
        }
    }
}
