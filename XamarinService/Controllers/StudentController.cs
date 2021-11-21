using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vives.BLL;
using Vives.DOMAIN;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XamarinService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        StudentManager studentManager = new StudentManager();

        // GET api/student/getbyid/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery(Name ="id")] int id)
        {
            try
            {
                Student student = await studentManager.GetByIdAsync(id);
                return Ok(new JsonResult(student));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get")]
        //[ValidateModel]
        public async Task<IActionResult> Get([FromQuery(Name = "skip")] int skip, [FromQuery(Name = "take")] int take)
        {
            try
            {
                if (take == 0)
                    take = 1;

                IEnumerable<Student> students = await studentManager.GetAsync(skip, take);
                return Ok(new JsonResult(students));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        //localhost:5000/api/student/create?Id=0&firstname=test&....
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            try
            {
                if (student == null)
                    throw new NullReferenceException();

                student = await studentManager.CreateAsync(student);
                return Ok(new JsonResult(student));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Update")]
        //[ValidateModel]
        public async Task<IActionResult> Update([FromBody] Student student)
        {
            try
            {
                if (student == null)
                    throw new NullReferenceException();

                student = await studentManager.UpdateAsync(student);
                return Ok(new JsonResult(student));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Delete")]
        //[ValidateModel]
        public async Task<IActionResult> Delete([FromBody] Student student)
        {
            try
            {
                if (student == null)
                    throw new NullReferenceException();

                student = await studentManager.DeleteAsync(student);
                return Ok(new JsonResult(student));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
