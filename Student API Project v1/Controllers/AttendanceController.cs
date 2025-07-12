using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Interfaces;
using StudentBusinessLayer.Services;
using StudentDataAccessLayer.Models;

namespace Student_API_Project_v1.Controllers
{

    [Route("api/Attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendancesService _attendancesService;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendancesService attendancesService  , IMapper mapper)
        {
            _mapper = mapper;
            _attendancesService = attendancesService;
        }

        [HttpGet("All", Name = "GetAllAttendances")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> GetAllAttendance()
        {

            var attendancesList = await _attendancesService.GetAllAttendances();
            if (attendancesList == null)
            {
                return NotFound("No Attendance Found!");
            }

            var dto = _mapper.Map<IEnumerable<AttendanceDTO>>(attendancesList);

            return Ok(dto);
        }




        [HttpGet("ByDate", Name = "GetAllAttendancesByDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> GetAllAttendanceByDate(DateTime date,string filterType)
        {

            try
            {

                var attendanceList = await _attendancesService.GetAllAttendancesByDate( date, filterType);

                if (attendanceList == null)
                return NotFound($"No Attendance with {date} are Found!");

                var dto = _mapper.Map<IEnumerable<AttendanceDTO>>(attendanceList);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex.Message}");
            }
        }
        
        
        
        
        [HttpGet("Paged", Name = "GetPaged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> GetAttendancePaged([FromQuery] int skip, [FromQuery] int take)
        {

            
            var attendancesList = await _attendancesService.GetPagedAsync(skip , take);
            if (attendancesList == null)
            {
                return NotFound("No Attendance Found!");
            }

            var dto = _mapper.Map<IEnumerable<AttendanceDTO>>(attendancesList);


            return Ok(dto);
        }



        [HttpGet("{ID}", Name = "GetAttendanceByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AttendanceDTO>> GetAttendanceById(int ID)
        {
            if (ID < 0)
                return BadRequest("Bad request");

            var attendance = await _attendancesService.GetAttendanceById(ID);

            if (attendance == null)
                return NotFound($"No Attendance with {ID} are Found!");

            var dto = _mapper.Map<AttendanceDTO>(attendance);

            return Ok(dto);

        }


        [HttpGet("ByDatePerStudent", Name = "GetAttendanceByDatePerStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> GetAttendanceByDatePerStudent( int studentId, [FromQuery]DateTime date , string filterType)
        {
            try
            {

                var attendanceList = await _attendancesService.GetAttendanceByDatePerStudent(studentId, date, filterType);

                if (attendanceList == null)
                    return NotFound($"No Attendance with {date} are Found!");


                var dto = _mapper.Map<IEnumerable<AttendanceDTO>>(attendanceList);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex.Message}");
            }

                

        }


        [HttpPost(Name = "AddAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<AttendanceDTO>> AddAttendance([FromBody] AttendanceDTO attendanceDTO)
        {
            if (attendanceDTO == null || attendanceDTO.StudentId <= 0)
                return BadRequest("Bad request");


            var attendanceEntity = _mapper.Map<Attendance>(attendanceDTO);

            var result = await _attendancesService.AddNewAttendancePerStudent(attendanceEntity);

           

            if (result == null)
                return BadRequest("Invalid operation"); 
            
            var dto = _mapper.Map<AttendanceDTO>(result);

            return Ok(dto);


        }
 



        [HttpDelete("{ID}", Name = "DeleteAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAttendance(int ID)
        {
            if (ID < 1)
            {
                return BadRequest($"Not accepted ID {ID}");
            }


            if (await _attendancesService.DeleteAttendance(ID))

                return Ok($"Attendance with ID {ID} has been deleted.");
            else
                return NotFound($"Attendance with ID {ID} not found. no rows deleted!");
        }




        [HttpGet("StudentCount", Name = "GetStudentCountAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetStudentCount(int studentId)
        {
            if (studentId < 0)
            {
                return BadRequest("Bad request");
            }


            var studentCount = await _attendancesService.CountAttendancePerStudent( studentId);
            if (studentCount < 0)
            {
                return NotFound($"No Attendance With StudentID {studentId}  are  Found!");
            }



            return Ok(studentCount);
        }

    }
}
