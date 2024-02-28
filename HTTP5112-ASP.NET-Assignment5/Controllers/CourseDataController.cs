using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using HTTP5112Assignment5.Models;
//using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;

namespace HTTP5112Assignment5.Controllers
{
    public class CourseDataController : Controller
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext schoolDb = new SchoolDbContext();
        
        //This Controller Will access the classes table of our blog database.
        /// <summary>
        /// Returns a list of Authors in the system
        /// </summary>
        /// <example>GET api/CourseData/ListCourse</example>
        /// <returns>
        /// A list of classes (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Course> ListCourse()
        {
            //Create an instance of a connection
            MySqlConnection Conn = schoolDb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Authors
            List<Course> Courses = new List<Course> {};

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int CourseId = (int)ResultSet["ClassId"];
                string CourseCode = ResultSet["ClassCode"].ToString().ToUpper();
                int TeacherId = Convert.ToInt32(ResultSet["TeacherId"]);
                string StartDate = ResultSet["StartDate"].ToString();
                string FinishDate = ResultSet["FinishDate"].ToString();
                string CourseName = ResultSet["ClassName"].ToString();


                Course NewCourse = new Course();
                NewCourse.CourseId = CourseId;
                NewCourse.CourseCode = CourseCode;
                NewCourse.TeacherId = TeacherId;
                NewCourse.StartDate = StartDate;
                NewCourse.FinishDate = FinishDate;
                NewCourse.CourseName = CourseName;

                //Add the Author Name to the List
                Courses.Add(NewCourse);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Courses;
        }


        /// <summary>
        /// Finds a class in the system given an ID
        /// </summary>
        /// <param name="id">The class primary key</param>
        /// <returns>A class object</returns>
        [HttpGet]
        public Course FindCourse(int id)
        {
            Course NewCourse = new Course();

            //Create an instance of a connection
            MySqlConnection Conn = schoolDb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes where ClassId = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int CourseId = (int)ResultSet["ClassId"];
                string CourseCode = ResultSet["ClassCode"].ToString().ToUpper();
                int TeacherId = Convert.ToInt32(ResultSet["TeacherId"]);
                string StartDate = ResultSet["StartDate"].ToString();
                string FinishDate = ResultSet["FinishDate"].ToString();
                string CourseName = ResultSet["ClassName"].ToString();

                NewCourse.CourseId = CourseId;
                NewCourse.CourseCode = CourseCode;
                NewCourse.TeacherId = TeacherId;
                NewCourse.StartDate = StartDate;
                NewCourse.FinishDate = FinishDate;
                NewCourse.CourseName = CourseName;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return NewCourse;
        }
    }
}
