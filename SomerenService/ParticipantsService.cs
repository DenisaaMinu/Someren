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

        public List<Student> GetParticipants(int activityId)
        {
            List<Participants> participantsInfo = participantdb.GetAllParticipants(activityId);
            List<Student> participants = new List<Student>();

            foreach (Participants participant in participantsInfo)
            {
                Student student = studentDao.GetStudentById(participant.StudentId);
                participants.Add(student);
            }

            return participants;
        }

        public List<Student> GetNonParticipants(int activityId)
        {
            //get information abput non-participants 
            List<Participants> nonPrticipantsInfo = participantdb.GetAllNonParticipants();
          
            //create a list to store non-participant students
            List<Student> nonParticipants = new List<Student>();

            foreach (Participants nonParticipant in nonPrticipantsInfo)
            {
                Student student = studentDao.GetStudentById(nonParticipant.StudentId);
                nonParticipants.Add(student);
            }

            return nonParticipants;
        }

        public Student GetStudentById(int id)
        {
            return studentDao.GetStudentById(id);
        }

        public void DeleteParticipants(int studentNumber, int activityNumber)
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
            
        }
    }
}
