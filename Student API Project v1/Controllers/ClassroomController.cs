using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Interfaces;
using StudentDataAccessLayer.Models;
using AutoMapper;
namespace Student_API_Project_v1.Controllers
{
    [Route("api/Classroom")]
    [ApiController]
    
    public class ClassroomController: ControllerBase
    {
        private readonly IClassroomService _classroomService;
        private readonly IMapper _mapper;

        public ClassroomController(IClassroomService classroomService, IMapper mapper)
        {
            _classroomService = classroomService;
            _mapper = mapper;
        }


        [HttpGet("All", Name = "GetAllClassrooms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ClassroomDTO>>> GetAllClassrooms()
        {

            var classroomList = await _classroomService.GetAllClassesAsync();
            if (classroomList == null)
            {
                return NotFound("No Classrooms are Found!");
            }
            var dto = _mapper.Map<IEnumerable<ClassroomDTO>>(classroomList);


            return Ok(dto);
        }




        [HttpGet("{ID}", Name = "GetClassroomByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClassroomDTOWithDetails>> GetClassroomByID(int ID)
        {
            if (ID < 0)
            {
                return BadRequest("Bad request");
            }

            var classroom = await _classroomService.GetClassByIdWithDetails(ID);
            if (classroom == null)
            {
                return NotFound($"No Classroom With {ID} are  Found!");
            }
            var dto = _mapper.Map<ClassroomDTOWithDetails>(classroom);


            return Ok(dto);
        }




        [HttpPost("Add", Name = "AddClassroom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClassroomDTO>> AddClassroom( ClassroomDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.name))
            {
                return BadRequest("Invalid classroom data!");
            }
            var classroom = _mapper.Map<Classroom>(dto);

            var result = await _classroomService.CreateClassroomAsync(classroom);

            if (result == null)
                return BadRequest("Failed to save");

            var resultDto = _mapper.Map<ClassroomDTO>(result);

            return Ok(resultDto);
        }



        [HttpPost("AssignTeacher", Name = "AssignTeacherToClassroom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AssignTeacherToClassroom(int classroomId , int teacherId)
        {
            if (classroomId  < 0 || teacherId < 0 )
            {
                return BadRequest("Invalid classroom data!");
            }



            var result = await _classroomService.AssignTeacherAsync(classroomId , teacherId);

            if (!result)
                return BadRequest("Failed to save");

            return Ok(result);
        }




        [HttpDelete("{ID}", Name = "DeleteClassroom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteClassroom(int ID)
        {
            if (ID < 1)
            {
                return BadRequest($"Not accepted ID {ID}");
            }


            if (await _classroomService.DeleteClassroomAsync(ID))

                return Ok($"Classroom with ID {ID} has been deleted.");
            else
                return NotFound($"Classroom with ID {ID} not found. no rows deleted!");
        }



        [HttpPut("{ID}", Name = "UpdateClassroom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateClassroom(int ID, string updatedClassroomName)
        {
            if (ID < 1 || string.IsNullOrWhiteSpace(updatedClassroomName))
            {
                return BadRequest("Invalid Classroom data.");
            }

            var result = await _classroomService.EditClassroomAsync(ID, updatedClassroomName);


            if (!result)
                return NotFound("Classroom not found.");


            return Ok();

        }


        [HttpGet("ByName/{Name}", Name = "GetClassroomByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClassroomDTOWithDetails>> GetClassroomByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Bad request");
            }


            var classroom = await _classroomService.GetClassByName(Name);
            if (classroom == null)
            {
                return NotFound($"No Classroom With {Name} are  Found!");
            }


            var dto = _mapper.Map<ClassroomDTOWithDetails>(classroom);

            return Ok(dto);
        }


    }
}
