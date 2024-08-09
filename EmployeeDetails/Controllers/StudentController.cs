using StudentDetails.Models;
using StudentDetails.Services;
using Microsoft.AspNetCore.Mvc;

namespace StudentDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            var students = await _studentService.GetAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student student)
        {
            await _studentService.CreateAsync(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Student student)
        {
            var existingStudent = await _studentService.GetAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            student.Id = id;
            await _studentService.UpdateAsync(id, student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            await _studentService.RemoveAsync(id);
            return NoContent();
        }
    }
}
