using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Interfaces;
using StudentBusinessLayer.Services;
using StudentDataAccessLayer.Models;

namespace StudentManagementAPI.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/Grade")]
    [ApiController]

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

            var studentsList = await _gradesService.GetAllGradesAsync();
            if (studentsList == null)
            {
                return NotFound("No Grades Found!");
            }

            var dto = _mapper.Map<IEnumerable<GradeDTO>>(studentsList);

            return Ok(dto);
        }

        [HttpGet("{ID}", Name = "GetGradeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GradeWithDetailsDTO>> GetGradeByID(int ID)
        {
            if (ID < 0)
            {
                return BadRequest("Bad request");
            }


            var grade = await _gradesService.GetGradeByIdAsync(ID);
            if (grade == null)
            {
                return NotFound($"No Grade With {ID} are  Found!");
            }
            var dto = _mapper.Map<GradeWithDetailsDTO>(grade);


            return Ok(dto);
        }




        [HttpPost("AddByObject",Name = "AddGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GradeDTO>> AddGradeByObject([FromBody] GradeDTO newGrade)
        {
            if (newGrade == null || newGrade.TeacherId < 0 || newGrade.StudentId < 0 || newGrade.SubjectId  < 0 || newGrade.Score < 0)
            {
                return BadRequest("Invalid grade data!");
            }
            var newGradeEntity = _mapper.Map<Grade>(newGrade);

            var result = await _gradesService .AddGradeAsync(newGradeEntity);

            if (result == null)
                return BadRequest("Failed to save");

            var resultDto = _mapper.Map<GradeDTO>(result);

            return Ok(resultDto);
        }
        
        
        [HttpPost("AddGrade",Name = "AddGrade.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GradeDTO>> AddGrade(int studentId , int subjectId , int teacherId , double score)
        {
            if ( teacherId < 0 || studentId < 0 ||subjectId  < 0 || score < 0)
            {
                return BadRequest("Invalid grade data!");
            }

            var result = await _gradesService .AddGradeAsync(studentId , teacherId , subjectId , score );

            if (result == null)
                return BadRequest("Failed to save");

            var resultDto = _mapper.Map<GradeDTO>(result);

            return Ok(resultDto);
        }




        [HttpDelete("DeleteByID", Name = "DeleteGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteGrade(int ID)
        {
            if (ID < 1)
            {
                return BadRequest($"Not accepted ID {ID}");
            }


            if (await _gradesService.DeleteGradeAsync(ID))

                return Ok($"Grade with ID {ID} has been deleted.");
            else
                return NotFound($"Grade with ID {ID} not found. no rows deleted!");
        }
        
        
        
        [HttpDelete("DeleteByStudentId", Name = "DeleteGradePerStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteGradPerStudent(int studentId , int subjectId)
        {
            if (studentId < 1 || subjectId < 1)
            {
                return BadRequest($"Not accepted ID {studentId} & {subjectId} ");
            }


            if (await _gradesService.DeleteGradeAsync(studentId,subjectId))

                return Ok($"Grade with ID {studentId} & {subjectId} has been deleted.");
            else
                return NotFound($"Grade with ID {studentId} & {subjectId} not found. no rows deleted!");
        }



        [HttpPut("{ID}", Name = "UpdateGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateStudent(int ID, GradeDTO  updateGradeDto)
        {
            if (ID < 1 || updateGradeDto == null || updateGradeDto.TeacherId < 0 ||
                updateGradeDto.StudentId < 0 || updateGradeDto.SubjectId < 0 || updateGradeDto.Score < 0)
            {
                return BadRequest("Invalid grade data.");
            }

            var gradeEntity = _mapper.Map<Grade>(updateGradeDto);
            gradeEntity.Id = ID;

            var result = await _gradesService.UpdateGradeAsync( gradeEntity);



            if (!result)
                return NotFound("Grade not found.");


            return Ok("Up to date successfully");

        }




    }
}
