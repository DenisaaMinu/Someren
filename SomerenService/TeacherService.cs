using SomerenDAL;
using SomerenModel;
using System.Collections.Generic;

namespace SomerenService
{
    public class TeacherService
    {
        private TeacherDAO teacherdb;
        public TeacherService()
        {
            teacherdb = new TeacherDAO();
        }

        public List<Teacher> GetAllTeachers(int activityNumber)
        {
            List<Teacher> teachers = teacherdb.GetAllTeachers();
            return teachers;
        }
        public List<Teacher> GetTeachers(int activityid)
        {
            List<Teacher> teachers = teacherdb.GetTeachers(activityid);
            return teachers;
        }
    }
}
