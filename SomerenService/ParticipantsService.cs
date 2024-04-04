using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenService
{
    public class ParticipantsService
    {
        private ParticipantsDAO participantdb;
        StudentDao studentDao = new StudentDao();

    
        public ParticipantsService()
        {
            participantdb = new ParticipantsDAO();
        }

        public void DeleteParticipants(int studentNumber, int activityNumber)
        {
            participantdb.DeleteParticipants(studentNumber, activityNumber);
        }

        public void AddParticipants(int studentNumber, int activityNumber)
        {
            participantdb.AddParticpants(studentNumber, activityNumber);
        }

    }
}
