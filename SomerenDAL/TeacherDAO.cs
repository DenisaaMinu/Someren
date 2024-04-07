using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;

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
        public List<Teacher> GetTeachers(int activityId)
        {
            string query = "SELECT * FROM STUDENT s " +
                           "JOIN SUPERVISION p ON s.[lecturerId] = p.[lecturerId] " +
                           "WHERE p.[activityId] = @activityId";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@activityId", SqlDbType.Int);
            sqlParameters[0].Value = activityId;

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            return ReadTables(dataTable);
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
    }
}