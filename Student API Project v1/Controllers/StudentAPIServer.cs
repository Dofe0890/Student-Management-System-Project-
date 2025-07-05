using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer.Services;
using StudentDomainLayer.Models;
using Models = StudentDomainLayer.Models;
using StudentBusinessLayer.Interfaces;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Model;
namespace StudentManagementAPI.Controllers
{
   // [Authorize (Roles = "Admin")]
    [Route("api/Students")]
    [ApiController]
    public class StudentAPIServer : ControllerBase
    {

        private readonly IStudentService _studentService;
        public StudentAPIServer (IStudentService studentService)
        { 
            _studentService = studentService;
        }


        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async  Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {

            var studentsList = await _studentService.GetAllStudents();
            if ( studentsList == null)
            {
                return NotFound("No Students Found!");
            }


            return Ok(studentsList);
        }



        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Student>>> GetPassedStudent(int? Skip , int?Take)
        {

            var studentsList = await _studentService.GetPassedStudents(Skip , Take);
            if (studentsList == null)
            {
                return NotFound("No Students Found!");
            }


            return Ok(studentsList);
        }



        [HttpGet("StudentByAgeOrder", Name = "GetStudentsByAgeOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentByOrderAge(int? Skip, int? Take)
        {

            var studentsList = await _studentService.GetStudentsByAgeOrder(Skip, Take);
            if (studentsList == null)
            {
                return NotFound("No Students Found!");
            }


            return Ok(studentsList);
        }



        //[HttpGet("AverageGrade", Name = "GetAverageGrade")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task < ActionResult<double>> GetAverageGrade()
        //{

        //    double AverageGrade =   _studentLogic.GetAverageGrade();
        //    return Ok(AverageGrade);
        //}




        [HttpGet("{ID}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> GetStudentByID(int ID)
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


            return Ok(student);
        }




        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task < ActionResult<Student>> AddStudent([FromBody]Student newStudent)
        {
            if (newStudent == null || string.IsNullOrEmpty(newStudent.Name) || newStudent.Age < 0 || newStudent.ClassroomId > 0)
            {
                return BadRequest("Invalid student data!");
            }

            var result = await _studentService.AddNewStudent(newStudent);

            if (result == null)
                return BadRequest("Failed to save");

            return Ok(result);
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
        public async Task < ActionResult<Student>> UpdateStudent(int ID, StudentDTO updatedStudent)
        {
            if (ID < 1 || updatedStudent == null || string.IsNullOrWhiteSpace(updatedStudent.Name)
                   || updatedStudent.Age < 0 || updatedStudent.ClassroomId > 0)
            {
                return BadRequest("Invalid student data.");
            }

            var result = await _studentService.EditStudent(ID, updatedStudent);



                if (!result)
                    return NotFound("Student not found.");
            

            return Ok(updatedStudent);

        }


        [HttpGet("StudentByName", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> GetStudentByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Bad request");
            }


            var student = await _studentService.GetStudentByName(Name);
            if (student == null)
            {
                return NotFound($"No StudentLogic With {Name} are  Found!");
            }


            return Ok(student);
        }

      

    }
}
