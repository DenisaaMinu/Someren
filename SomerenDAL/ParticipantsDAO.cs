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

        public void DeleteParticipants(int studentNumber, int activityNumber)
        {
            string query = "DELETE FROM PARTICIPANTS WHERE [studentId] = @studentId AND [activityId] = @activityId";
            SqlParameter[] sqlParameters ={
            new SqlParameter("@studentId", studentNumber),
            new SqlParameter("@activityId", activityNumber)
            };

            // Execute the SQL DELETE statement
            ExecuteEditQuery(query, sqlParameters);
        }


        public void AddParticpants(int studentNumber, int activityNumber)
        {
            string query = "INSERT INTO PARTICIPANTS ([studentId], [activityId])VALUES (@studentId, @activityId)";
            SqlParameter[] sqlParameters ={
            new SqlParameter("@studentId", studentNumber),
            new SqlParameter("@activityId", activityNumber)
           };

            // Execute the SQL INSERT statement
            ExecuteEditQuery(query, sqlParameters);
        }

    }



}

