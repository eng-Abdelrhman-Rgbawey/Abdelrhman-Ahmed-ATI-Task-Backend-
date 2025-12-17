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
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StudentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // POST: api/Students -- Create new student
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentDto dto)
        {
            if (dto.CourseIds.Count < 1 || dto.CourseIds.Count > 2)
                return BadRequest("Student must select 1 or 2 courses only");

            var student = _mapper.Map<Student>(dto);

            student.StudentCourses = dto.CourseIds
                .Select(id => new StudentCourse { CourseId = id })
                .ToList();

            await _unitOfWork.Students.Add(student);
            await _unitOfWork.Complete();

            return Ok(new { message = "Student Creatied successfully" });
        }

    }
}
