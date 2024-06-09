using SomerenModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenDAL
{
    public class SupervisorDAO : BaseDao
    {
        public List<Supervisors> GetSupervisor()
        {
            string query = @"SELECT s.lectureID, l.firstName, l.lastName
            FROM dbo.supervise s
            JOIN dbo.LECTURER l ON s.lectureID = l.lecturerId";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadSupervisors(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Supervisors> GetSupervisorActivities()
        {
            string query = @"SELECT a.activityID, a.name, s.lectureID, l.firstName, l.lastName
            FROM dbo.supervise s
            JOIN dbo.ACTIVITY a ON s.activityID = a.activityId
            JOIN dbo.LECTURER l ON s.lectureID = l.lecturerId";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadSupervisorActivities(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Supervisors> GetNotSupervisor()
        {
            string query = @"SELECT  l.lecturerId, l.firstName ,  l.lastName 
            FROM dbo.LECTURER l WHERE l.lecturerId NOT IN (SELECT lectureID FROM dbo.supervise)";
           // LEFT JOIN dbo.supervise s ON l.lecturerId = s.lectureID
           // WHERE s.lecturerID IS NULL";
            SqlParameter[] sqlParameters = new SqlParameter[0];

            return ReadNotSupervisors(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Supervisors> ReadSupervisors(DataTable dataTable)
        {
            List<Supervisors> supervisors = new List<Supervisors>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Supervisors supervisor = new Supervisors()
                {
                    lectureId = (int)dr["lectureID"],
                    firstName = dr["FirstName"].ToString(),
                    lastName = dr["LastName"].ToString()
                };

                supervisors.Add(supervisor);
            }
            return supervisors;
        }

        private List<Supervisors> ReadSupervisorActivities(DataTable dataTable)
        {
            List<Supervisors> supervisorActivities = new List<Supervisors>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Supervisors supervisorActivity = new Supervisors()
                {
                    ActivityID = (int)dr["activityID"],
                    
                    lectureId = (int)dr["lectureID"],
                    
                };
                supervisorActivities.Add(supervisorActivity);
            }
            return supervisorActivities;
        }

        private List<Supervisors> ReadNotSupervisors(DataTable dataTable)
        {
            List<Supervisors> supervisors = new List<Supervisors>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Supervisors supervisor = new Supervisors()
                {
                    lectureId = (int)dr["lecturerId"],
                    firstName = dr["FirstName"].ToString(),
                    lastName = dr["LastName"].ToString()
                };

                supervisors.Add(supervisor);
            }
            return supervisors;
        }
        protected void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            ExecuteEditQuery(query, parameters);
        }
        public void RemoveSupervisor(int activityID, int lecturerID)
        {
            string query = "DELETE FROM dbo.supervise WHERE activityID = @ActivityID AND lectureID = @LectureID";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@ActivityID", activityID),
            new SqlParameter("@LectureID", lecturerID)
            };
            ExecuteNonQuery(query, parameters);
        }

        public void AddSupervisor(int activityID, int lecturerID)
        {
            string query = "INSERT INTO dbo.supervise(activityID, lectureID) VALUES(@ActivityID, @LectureID)";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@ActivityID", SqlDbType.Int) { Value = activityID },
        new SqlParameter("@LectureID", SqlDbType.Int) { Value = lecturerID }
            };
            ExecuteNonQuery(query, parameters);
        }

        
        }
    }


