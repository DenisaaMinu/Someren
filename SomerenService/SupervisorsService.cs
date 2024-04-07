using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;

namespace SomerenService
{
    internal class SupervisorsService
    {
        private Teacher teacherdb;

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
