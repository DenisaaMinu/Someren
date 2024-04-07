using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenDAL
{
    public class SupervisorDAO : BaseDao
        {

            public void DeleteSupervisors(int studentNumber, int activityNumber)
            {
                string query = "DELETE FROM PARTICIPANTS WHERE [studentId] = @studentId AND [activityId] = @activityId";
                SqlParameter[] sqlParameters ={
        new SqlParameter("@studentId", studentNumber),
        new SqlParameter("@activityId", activityNumber)
    };

                // Execute the SQL DELETE statement
                ExecuteEditQuery(query, sqlParameters);
            }


            public void AddSupervisors(int teacherId, int activityNumber)
            {
                string query = "INSERT INTO Supervisors ([Teacherid], [activityId])VALUES (@teacherId, @activityId)";
                SqlParameter[] sqlParameters ={
            new SqlParameter("@teacherId", teacherId),
            new SqlParameter("@activityId", activityNumber)
           };

                // Execute the SQL INSERT statement
                ExecuteEditQuery(query, sqlParameters);
            }

        }
    }

