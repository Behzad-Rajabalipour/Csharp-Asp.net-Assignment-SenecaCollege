using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using HTTP5112Assignment5.Models;
using System.Diagnostics;

namespace HTTP5112Assignment5.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeacher(SearchKey);
            ViewBag.SearchKey = SearchKey;
            return View(Teachers);
        }

        //GET : /Tecaher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            IEnumerable<Course> Courses = controller.ListCoursesForTeacher(id);
            ViewBag.courses = Courses;

            return View(NewTeacher);
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, string HireDate, string Salary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form
             
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            DateTime HireDateDT;
            if (!DateTime.TryParse(HireDate, out HireDateDT))
            {
                NewTeacher.HireDate = null;
            }
            if (!float.TryParse(Salary, out NewTeacher.Salary))
            {
                NewTeacher.Salary = float.NaN;
            }

            if (NewTeacher.TeacherFname == null || NewTeacher.TeacherLname == null || NewTeacher.EmployeeNumber == null || NewTeacher.HireDate == null || Salary == null)
            {
                ViewBag.from = "Create";
                return View("Error", NewTeacher);
            }
            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the Teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receives a POST request containing information about an existing Teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated Teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the Teacher</param>
        /// <param name="TeacherLname">The updated last name of the Teacher</param>
        /// <param name="EmployeeNumber">The updated bio of the Teacher.</param>
        /// <param name="HireDate">The updated email of the Teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the Teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Raymond",
        ///	"TeacherLname":"Lee",
        ///	"EmployeeNumber":"T123",
        ///	"HireDate":"2022-11-23"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, string HireDate, string Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.TeacherId = id;
            DateTime HireDateDT;
            if (!DateTime.TryParse(HireDate, out HireDateDT))
            {
                TeacherInfo.HireDate = null;
            }
            else
            {
                // Parse date to be in valid format before inserting into database
                Debug.WriteLine("HireDateDT");
                Debug.WriteLine(HireDateDT);
                TeacherInfo.HireDate = HireDateDT.ToString("yyyy/MM/dd HH:mm:ss");
                Debug.WriteLine(TeacherInfo.HireDate);
            }
            if (!float.TryParse(Salary, out TeacherInfo.Salary))
            {
                TeacherInfo.Salary = float.NaN;
            }
            if (TeacherInfo.TeacherFname == null || TeacherInfo.TeacherLname == null || TeacherInfo.EmployeeNumber == null || TeacherInfo.HireDate == null || Salary == null)
            {
                ViewBag.from = "Update";
                return View("Error", TeacherInfo);
            }
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            // return RedirectToAction("Show/" + id);
            // RedirectToAction automatically encodes / to %2F in the url which is not identified by the server
            // Use RedirectToAction(string, Object) insted to redirect with route values.
            // Documentation: https://learn.microsoft.com/en-us/dotnet/api/system.web.mvc.controller.redirecttoaction?view=aspnet-mvc-5.2
            return RedirectToAction("Show", new { id = id});
        }
    }
}