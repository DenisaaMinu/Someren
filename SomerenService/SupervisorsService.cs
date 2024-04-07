using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenService
{
    internal class SupervisorsService
    {
        private TeacherDAO teacherdb;

        public List<SupervisorsService> GetAllSupervisors(int activityNumber)
        {
            List<SupervisorsService> Supervisors = teacherdb.GetAllTeachers();
            return Supervisors;
        }
        public List<SupervisorsService> GetSupervisors(int activityId)
        {
            List<SupervisorsService> Supervisors = teacherdb.GetTeachers(activityId);
            return Supervisors;
        }
    }
}
