using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;
using SomerenDAL;
using System;

namespace SomerenDAL
{
    public class TeacherDAO : BaseDao
    {
        public List<Teacher> GetAllTeachers()
        {
            string query = "SELECT lecturerId, roomId, firstName, lastName, phoneNumber, age FROM [LECTURER]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Teacher> ReadTables(DataTable dataTable)
        {
            List<Teacher> teachers = new List<Teacher>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Teacher teacher = new Teacher()
                {
                    Id = (int)dr["lecturerId"],
                    RoomId = (int)dr["roomId"],
                    FirstName = dr["firstName"].ToString(),
                    LastName = dr["lastName"].ToString(),
                    TelephoneNumber = dr["phoneNumber"].ToString(),
                    Age = (int)dr["age"]
                };
                teachers.Add(teacher);
            }
            return teachers;
        }
        public List<Teacher> GetSupervisors(int activityId)
        {
            string query = "SELECT * FROM LECTURER AS L " +
                    "JOIN SUPERVISION AS S ON L.[lecturerId] = S.[lecturerId] " +
                    "WHERE S.[activityId] = @activityId";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@activityId", SqlDbType.Int);
            sqlParameters[0].Value = activityId;

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
            return ReadTables(dataTable);
        }
        public List<Teacher> GetNonSupervisorsByActivity(int activityId)
        {
            string query = "SELECT * FROM LECTURER AS L " +
                           "WHERE L.[lecturerId] NOT IN (SELECT [lecturerId] FROM SUPERVISION WHERE [activityId] = @activityId)";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@activityId", SqlDbType.Int);
            sqlParameters[0].Value = activityId;

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
            return ReadTables(dataTable);
        }
    }
}