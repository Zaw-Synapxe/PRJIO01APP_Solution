using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManyToManyController(AppDbContext context) : ControllerBase
    {
        [HttpPost("add-student")]
        public async Task<IActionResult>CreateStudent(Student student)
        {
            context.Students.Add(student);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-student")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await context.Students.Include(x => x.CoursesStudents).ToListAsync());
        }

        [HttpPost("add-course")]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            context.Courses.Add(course);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-course")]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await context.Courses.Include(x => x.CoursesStudents).ToListAsync());
        }

        [HttpPost("add-course-student")]
        public async Task<IActionResult> CreateStudentCourse(CourseStudent studentcourse)
        {
            context.CoursesStudents.Add(studentcourse);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-courses-students")]
        public async Task<IActionResult> GetStudentsCourses()
        {
            return Ok(await context.CoursesStudents
                .Include(x => x.Course)
                .Include(x => x.Student)
                .ToListAsync());
        }

        //
    }

    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<CourseStudent>? CoursesStudents { get; set; }
    }

    public class Course
    {
        public int Id { get; set; } 
        public string? Title { get; set; }
        public ICollection<CourseStudent>? CoursesStudents { get; set; }
    }

    public class CourseStudent
    {
        public int Id { get; set; } // P Key
        public int StudentId { get; set; } // Composite key
        public int CourseId { get; set; } // Composite key
        public Student? Student { get; set; } //Navigation Property
        public Course? Course { get; set; } //Navigation Property

    }

    public partial class AppDbContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseStudent> CoursesStudents => Set<CourseStudent>();

    }

    //
}
