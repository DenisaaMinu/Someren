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

        public ParticipantsService()
        {
            participantdb = new ParticipantsDAO();
        }

        public List<Participants> GetParticipants()
        {
            List<Participants> participants = participantdb.GetAllParticipants();
            return participants;
        }

        public void AddParticipant(Participants participant)
        {
            participantdb.AddParticipant(participant);
        }

        public void DeleteParticipant(Participants participant) 
        { 
            participantdb.DeleteParticipant(participant);
        }
    }
}
