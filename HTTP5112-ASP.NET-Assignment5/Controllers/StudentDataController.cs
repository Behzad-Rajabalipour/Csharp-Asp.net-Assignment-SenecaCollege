using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using HTTP5112Assignment5.Models;
using MySql.Data.MySqlClient;

namespace HTTP5112Assignment5.Controllers
{
    public class StudentDataController : Controller
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext schoolDb = new SchoolDbContext();

        //This Controller Will access the students table of our blog database.
        /// <summary>
        /// Returns a list of students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudent</example>
        /// <returns>
        /// A list of students (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Student> ListStudent()
        {
            //Create an instance of a connection
            MySqlConnection Conn = schoolDb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Authors
            List<Student> Students = new List<Student> {};

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["StudentId"]);
                string StudentFname = ResultSet["StudentFname"].ToString();
                string StudentLname = ResultSet["StudentLname"].ToString();
                string StudentNumber = ResultSet["StudentNumber"].ToString();
                string EnrolDate = ResultSet["EnrolDate"].ToString();

                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;

                //Add the student object to the List
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of student objects
            return Students;
        }


        /// <summary>
        /// Finds a student in the system given an ID
        /// </summary>
        /// <param name="id">The student primary key</param>
        /// <returns>A student object</returns>
        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Create an instance of a connection
            MySqlConnection Conn = schoolDb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students where StudentId = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["StudentId"]);
                string StudentFname = ResultSet["StudentFname"].ToString();
                string StudentLname = ResultSet["StudentLname"].ToString();
                string StudentNumber = ResultSet["StudentNumber"].ToString();
                string EnrolDate = ResultSet["EnrolDate"].ToString();

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return NewStudent;
        }

    }
}
