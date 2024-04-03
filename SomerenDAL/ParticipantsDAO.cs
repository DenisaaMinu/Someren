using SomerenModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenDAL
{
    public class ParticipantsDAO : BaseDao
    {
        public List<Participants> GetAllParticipants() 
        {
            string query = "SELECT activityId, studentId FROM [PARTICIPANTS]";

            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Participants> ReadTables(DataTable dataTable)
        {
            List<Participants> participants = new List<Participants>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Participants participant = new Participants()
                {
                    ActivityId = (int)dr["activityId"],
                    StudentId = (int)dr["studentId"]
                };
                participants.Add(participant);
            }
            return participants;
        }

        public void AddParticipant(Participants participants)
        {
            try
            {
                string query = "INSERT INTO PARTICIPANTS (studentId)" +
                               "VALUES (@StudentId)";


                SqlParameter[] parameters =
                {
                  new SqlParameter("@ActivityId", participants.ActivityId),
                  new SqlParameter("@StudentId", participants.StudentId),
                };
                ExecuteEditQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }

        public void DeleteParticipant(Participants participants)
        {
            try
            {

                string query = "DELETE FROM [PARTICIPANTS]" +
                               "WHERE activityId = @ActivityId";

                SqlParameter[] sqlParameters =
                {
                new SqlParameter("@ActivityId", participants.ActivityId)
                };

                ExecuteEditQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
