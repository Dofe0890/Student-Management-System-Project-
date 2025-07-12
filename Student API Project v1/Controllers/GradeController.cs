using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Interfaces;

namespace StudentManagementAPI.Controllers
{
    public class GradeController:ControllerBase

    {
        private readonly IGradesService _gradesService;
        private readonly IMapper _mapper;
        public GradeController(IGradesService gradesService, IMapper mapper)
        {
            _gradesService = gradesService;
            _mapper = mapper;
        }


        [HttpGet("All", Name = "GetAllGrades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GradeDTO>>> GetAllStudents()
        {

            var studentsList = await _gradesService.GetAllGrades();
            if (studentsList == null)
            {
                return NotFound("No Students Found!");
            }

            var dto = _mapper.Map<IEnumerable<StudentDTO>>(studentsList);

            return Ok(dto);
        }






    }
}
