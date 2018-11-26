using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrandCircus.Models;

namespace GrandCircus.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            return View();
        }

        public ActionResult EditStudent(int ID)
        {
            GCUniEntities ORMstudent = new GCUniEntities();

            //passing the Item ID to a viewbag
            ViewBag.ThisStudent = ORMstudent.Students.Find(ID);
            return View();
        }

        public ActionResult EditStudentForm(Student oldStudent)
        {
            GCUniEntities ORMstudent = new GCUniEntities();
            Student newStudent = ORMstudent.Students.Find(oldStudent.ID);

            newStudent.fName = oldStudent.fName;
            newStudent.lName = oldStudent.lName;
            newStudent.phoneNumber = oldStudent.phoneNumber;
            newStudent.address = oldStudent.address;

            ORMstudent.Entry(newStudent).State = System.Data.Entity.EntityState.Modified;
            ORMstudent.SaveChanges();

            return RedirectToAction("StudentList");
        }

        public ActionResult EditCourseForm(Course oldCourse)
        {

            GCUniEntities ORMcourse = new GCUniEntities();
            Course newCourse = ORMcourse.Courses.Find(oldCourse.ID);

            newCourse.Name = oldCourse.Name;
            newCourse.Category = oldCourse.Category;


            ORMcourse.Entry(newCourse).State = System.Data.Entity.EntityState.Modified;
            ORMcourse.SaveChanges();

            return RedirectToAction("ListCourses");
        }

        public ActionResult EditCourse(int ID)
        {
            GCUniEntities ORMcourse = new GCUniEntities();

            //passing the Item ID to a viewbag
            ViewBag.ThisCourse = ORMcourse.Courses.Find(ID);
            return View();
        }
        public ActionResult AddStudent()
        {

            return View();

        }

        public ActionResult AddStudentForm(Student newStudent)
        {
            GCUniEntities ORMadd = new GCUniEntities();

            //add a student from new student object student
            ORMadd.Students.Add(newStudent);
            ORMadd.SaveChanges();

            return RedirectToAction("StudentList");
        }

        public ActionResult AddCourseForm(Course newCourse)
        {
            GCUniEntities ORMadd = new GCUniEntities();

            //add a student from new student object student
            ORMadd.Courses.Add(newCourse);
            ORMadd.SaveChanges();

            return RedirectToAction("ListCourses");
        }

        public ActionResult AddCourse()
        {
            return View();

        }
        public ActionResult StudentList()
        {
            //ORM object relational mapping
            GCUniEntities listORM = new GCUniEntities();

            //putting all eneities of the student db into a viewbag.
            ViewBag.AllStudents = listORM.Students.ToList();

            return View();
        }

        public ActionResult ListCourses()
        {
            //ORM object relational mapping
            GCUniEntities listCourseORM = new GCUniEntities();

            //putting all eneities of the student db into a viewbag.
            ViewBag.AllCourses = listCourseORM.Courses.ToList();

            return View();
        }

        public ActionResult DeleteStud(int ID)
        {
            //ORM object relational mapping
            GCUniEntities listORM = new GCUniEntities();

            //making a list where the method int ID == the student ID then put it into StudentCourseList using
            //finds and compares it to studentcourses using the for.. key in student cs.
            List<StudentCourse> StudentCourseList = listORM.Students.Find(ID).StudentCourses.ToList();
            listORM.StudentCourses.RemoveRange(StudentCourseList);

            //finds student ID
            Student findStudent = listORM.Students.Find(ID);

            //delete
            listORM.Entry(findStudent).State = System.Data.Entity.EntityState.Deleted;
            //save changes
            listORM.SaveChanges();

            return RedirectToAction("StudentList");
        }

        public ActionResult DeleteCourse(int ID)
        {
            //ORM object relational mapping
            GCUniEntities listORM = new GCUniEntities();

            //making a list where the method int ID == the course ID then put it into StudentCourseList
            List<StudentCourse> StudentCourseList = listORM.StudentCourses.Where(x => x.CourseID == ID).ToList();

            //removing the data in the database where the list matches the database.
            foreach (StudentCourse sc in StudentCourseList)
            {
                listORM.StudentCourses.Remove(sc);
            }

            Course findCourse = listORM.Courses.Find(ID);

            //delete using remove
            listORM.Courses.Remove(findCourse);
            //save changes
            listORM.SaveChanges();

            return RedirectToAction("ListCourses");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}