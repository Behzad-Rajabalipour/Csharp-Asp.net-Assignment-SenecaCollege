using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
using HTTP5112Assignment5.Models;

namespace HTTP5112Assignment5.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Course/List
        public ActionResult List()
        {
            CourseDataController controller = new CourseDataController();
            IEnumerable<Course> Courses = controller.ListCourse();
            return View(Courses);
        }

        //GET : /Course/Show/{id}
        public ActionResult Show(int id)
        {
            CourseDataController courseController = new CourseDataController();
            Course NewCourse = courseController.FindCourse(id);
            TeacherDataController teacherController = new TeacherDataController();
            Teacher Teacher = teacherController.FindTeacher(NewCourse.TeacherId);
            ViewBag.Teacher = Teacher;

            return View(NewCourse);
        }
    }
}