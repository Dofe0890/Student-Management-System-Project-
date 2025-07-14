using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Interfaces;

namespace StudentManagementAPI.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        public SubjectController(ISubjectService  subjectService, IMapper mapper)
        {
            _mapper = mapper;
            _subjectService = subjectService;
        }




        [HttpGet("All", Name = "GetAllSubjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetAllSubjects()
        {
            var result = await _subjectService.GetAllSubjectsAsync();
            if (result == null)

                return BadRequest("No subjects are found.");

            var dto = _mapper.Map<IEnumerable<SubjectDTO>>(result);

            return Ok(dto);

        }




        [HttpGet("{ID}", Name = "GetSubjectById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubjectDTO>> GetSubjectById(int ID)
        {

            if (ID <= 0)
                return BadRequest("ID must be greater than zero.");


            var result = await _subjectService.GetSubjectByIdAsync(ID);


            if (result == null)
                return BadRequest("No subjects are found.");


            var dto = _mapper.Map<SubjectDTO>(result);


            return Ok(dto);
        }





        [HttpGet("ByName", Name = "GetSubjectByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubjectDTO>> GetTeacherByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                return BadRequest("Name cannot be null or empty.");

            var result = await _subjectService.GetSubjectByNameAsync(Name);
            if (result == null)
                return BadRequest("No subjects are found.");


            var dto = _mapper.Map<SubjectDTO>(result);


            return Ok(dto);
        }


    }
}
