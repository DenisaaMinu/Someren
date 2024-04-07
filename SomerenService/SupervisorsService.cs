using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenService
{
    public class SupervisorsService
    {
        private SupervisorDAO supervisordb;

        public SupervisorsService()
        {
            supervisordb = new SupervisorDAO();
        }
        public List<Supervisors> GetNotSupervisors()
        {
            List<Supervisors> supervisors = supervisordb.GetNotSupervisor();
            return supervisors;
        }
        public List<Supervisors> GetSupervisors()
        {
            List<Supervisors> supervisors = supervisordb.GetSupervisor();
            return supervisors;
        }

        public List<Supervisors> GetSupervisorActivities()
        {
            List<Supervisors> activities = supervisordb.GetSupervisorActivities();
            return activities;
        }

        public void RemoveSupervisor(int activityID, int lecturerID)
        {
            supervisordb.RemoveSupervisor(activityID, lecturerID);
        }

    }
}
