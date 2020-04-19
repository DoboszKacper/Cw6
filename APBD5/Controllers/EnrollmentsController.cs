using APBD5.Helpers;
using APBD5.DTOs.RequestModels;
using APBD5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APBD5.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {

        private readonly IStudentsDbService _dbService;

        public EnrollmentsController(IStudentsDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public IActionResult CreateStudent(StudentWithStudiesRequest request)
        {
            try
            {
                return Ok(_dbService.CreateStudentWithStudies(request));
            }
            catch (DbServiceException e)
            {
                if (e.Type == DbServiceExceptionTypeEnum.NotFound)
                    return NotFound(e.Message);
                else if (e.Type == DbServiceExceptionTypeEnum.ValueNotUnique)
                    return BadRequest(e.Message);
                else
                    return StatusCode(500);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromotionRequest request)
        {
            if (!_dbService.CheckIfEnrollmentExists(request.Studies, request.Semester))
                return NotFound("Enrollment not found.");

            try
            {
                return Ok(_dbService.PromoteStudents(request.Studies, request.Semester));
            }
            catch (DbServiceException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}