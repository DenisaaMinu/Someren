using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;

namespace SomerenService
{
    public class TeacherService
    {
        private TeacherDAO teacherDao;

        public TeacherService()
        {
            teacherDao = new TeacherDAO();
        }

        public List<Teacher> GetTeachers()
        {
            return teacherDao.GetAllTeachers();
        }

        public List<Teacher> GetSupervisors(int activityId)
        {
            return teacherDao.GetSupervisors(activityId);
        }

        public List<Teacher> GetNonSupervisors(int activityId)
        {
            return teacherDao.GetNonSupervisors(activityId);
        }
    }
}
