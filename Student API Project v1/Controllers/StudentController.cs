using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer.Services;
using StudentDataAccessLayer.Models;
using Models = StudentDataAccessLayer.Models;
using StudentBusinessLayer.Interfaces;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Model;
using AutoMapper;
namespace StudentManagementAPI.Controllers
{
   // [Authorize (Roles = "Admin")]
    [Route("api/Students")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentController (IStudentService studentService,IMapper mapper)
        { 
            _studentService = studentService;
            _mapper = mapper;
        }


        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async  Task<ActionResult<IEnumerable<StudentDTO>>> GetAllStudents()
        {

            var studentsList = await _studentService.GetAllStudents();
            if ( studentsList == null)
            {
                return NotFound("No Students Found!");
            }

            var dto = _mapper.Map<IEnumerable<StudentDTO>>(studentsList);

            return Ok(dto);
        }


      


        [HttpGet("StudentByAgeOrder", Name = "GetStudentsByAgeOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentByOrderAge(int? Skip, int? Take)
        {

            var studentsList = await _studentService.GetStudentsByAgeOrder(Skip, Take);
            if (studentsList == null)
            {
                return NotFound("No Students Found!");
            }

            var dto = _mapper.Map<IEnumerable<StudentDTO>>(studentsList);

            return Ok(dto);
        }




        [HttpGet("{ID}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> GetStudentByID(int ID)
        {
            if (ID < 0)
            {
                return BadRequest("Bad request");
            }


            var student = await _studentService.GetStudentById(ID);
            if (student == null)
            {
                return NotFound($"No StudentLogic With {ID} are  Found!");
            }
            var dto = _mapper.Map<StudentDTO>(student);


            return Ok(dto);
        }




        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task < ActionResult<StudentDTO>> AddStudent([FromBody]StudentDTO newStudentDTO)
        {
            if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.Age < 0 || newStudentDTO.ClassroomId < 0)
            {
                return BadRequest("Invalid student data!");
            }

            var newStudent = _mapper.Map<Student>(newStudentDTO);

            var result = await _studentService.AddNewStudent(newStudent);

            if (result == null)
                return BadRequest("Failed to save");

            var resultDto = _mapper.Map<StudentDTO>(result);

            return Ok(resultDto);
        }




        [HttpDelete("{ID}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <  ActionResult > DeleteStudent(int ID)
        {
            if (ID < 1)
            {
                return BadRequest($"Not accepted ID {ID}");
            }


            if ( await _studentService.DeleteStudent(ID))

                return Ok($"StudentLogic with ID {ID} has been deleted.");
            else
                return NotFound($"StudentLogic with ID {ID} not found. no rows deleted!");
        }



        [HttpPut("{ID}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task < ActionResult<StudentDTO>> UpdateStudent(int ID, StudentDTO updatedStudent)
        {
            if (ID < 1 || updatedStudent == null || string.IsNullOrWhiteSpace(updatedStudent.Name)
                   || updatedStudent.Age < 0 || updatedStudent.ClassroomId < 0)
            {
                return BadRequest("Invalid student data.");
            }

            var result = await _studentService.EditStudent(ID, updatedStudent);



                if (!result)
                    return NotFound("Student not found.");
            

            return Ok(updatedStudent);

        }



      

    }
}
