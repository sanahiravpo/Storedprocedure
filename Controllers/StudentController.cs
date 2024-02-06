using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storedprocedure.Models;

namespace Storedprocedure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase

    {
        private IStudent _student;
        public StudentController(IStudent student)
        {
            _student = student;
        }
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            _student.AddStudent(student);
            return Ok();
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_student.GetStudents());
        }

        [HttpDelete("{id}")]
        public IActionResult deletestudent(int id)
        {
            _student.deletestudent(id);
            return NoContent();
        }
        [HttpPut]
        public IActionResult updateStudent([FromBody] Student updatestudent,int id) {

            _student.updateStudent( updatestudent,id);
            return Ok();
        }
    }
}
