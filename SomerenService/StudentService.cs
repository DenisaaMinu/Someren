using SomerenDAL;
using SomerenModel;
using System.Collections.Generic;

namespace SomerenService
{
    public class StudentService
    {
        private StudentDao studentdb;

        public StudentService()
        {
            studentdb = new StudentDao();
        }

        public List<Student> GetStudents()  
        { 
            List<Student> students = studentdb.GetAllStudents();
            return students;
        }
        public void AddStudent(Student student)
        {
            studentdb.AddStudent(student);
        }

        public void DeleteStudent(Student student)
        {
            studentdb.DeleteStudent(student);
        }

        public void ModifyStudent(Student student)
        {
            studentdb.ModifyStudent(student);
        }

        public List<Student> GetByActivityId(int activityNumber)
        {
            List<Student> students = studentdb.GetParticipants(activityNumber);
            return students;
        }


    }

}