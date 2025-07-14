using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Interfaces;

namespace StudentManagementAPI.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _mapper = mapper;
            _teacherService = teacherService;
        }




        [HttpGet("All",Name = "GetAllTeachers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TeacherDTO>>> GetAllTeachers()
        {
            var result = await _teacherService.GetAllTeachers();
            if(result == null)
                return BadRequest("No teachers found.");

            var dto = _mapper.Map<IEnumerable<TeacherDTO>>(result);

            return Ok(dto);

        } 


        
        
        [HttpGet("{ID}",Name = "GetTeacherById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherWithDetailsDTO>> GetTeacherById(int ID)
        {

            if(ID <= 0)
                return BadRequest("ID must be greater than zero.");


            var result = await _teacherService.GetTeacherById(ID);


            if(result == null)
                return BadRequest("No teacher found.");


            var dto = _mapper.Map<TeacherWithDetailsDTO>(result);


            return Ok(dto);
        }
        
                
        
        
        
        [HttpGet("ByName",Name = "GetTeacherByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherWithDetailsDTO>> GetTeacherByName(string Name)
        {
            if(string.IsNullOrEmpty(Name))
                return BadRequest("Name cannot be null or empty.");

            var result = await _teacherService.GetTeacherByName(Name);
            if(result == null)
                return BadRequest("No teacher found.");


            var dto = _mapper.Map<TeacherWithDetailsDTO>(result);


            return Ok(dto);
        }
        
        
        
        
        [HttpPut("EditTeacher",Name = "EditTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditTeacher(int ID , TeacherDTO dto )
        {
            if (dto == null || ID <= 0)
                return BadRequest("No teacher found.");

             var result = await _teacherService.EditTeacher(ID ,dto );


            if(!result)
                return BadRequest("No teacher found.");

            return Ok("Teacher up to date successfully");
        }
        
                       

    }
}
