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
        public List<Participants> GetAllParticipants(int activityId)
        {
            string query = "SELECT A.activityId, S.studentId FROM ACTIVITY AS A " +
                                  "JOIN PARTICIPANTS AS P ON P.activityId = A.activityId " +
                                  "JOIN STUDENT AS S ON P.studentId = S.studentId " +
                                  "WHERE A.activityId = @ActivityId";

            SqlParameter[] sqlParameters = {
        new SqlParameter("@ActivityId", SqlDbType.Int) { Value = activityId }
    };

            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Participants> GetAllNonParticipants()
        {
            string query = "SELECT  A.activityId, S.studentId FROM ACTIVITY AS A " +
                                 "JOIN STUDENT AS S ON 1 = 1 " +
                           "WHERE NOT EXISTS " +
                                             "(SELECT 1 FROM PARTICIPANTS AS P " +
                                             "WHERE P.activityId = A.activityId AND P.studentId = S.studentId)";

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

            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Participants> ReadTables(DataTable dataTable)
        {
            string query = "DELETE FROM PARTICIPANTS WHERE [studentId] = @studentId AND [activityId] = @activityId";
            
            SqlParameter[] sqlParameters =
            {
               new SqlParameter("@studentId", studentNumber),
               new SqlParameter("@activityId", activityNumber)
            };

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

        public void AddParticpants(int studentNumber, int activityNumber)
        {
            string query = "INSERT INTO PARTICIPANTS (activityId, studentId)" +
                           "VALUES (@activityId, @studentId)";
            
            SqlParameter[] sqlParameters =
            {
              new SqlParameter("@studentId", studentNumber),
              new SqlParameter("@activityId", activityNumber)
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
