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
            string query = "SELECT first_name, last_name, roomID, telephone_phoneNumber, age FROM [LECTURER]";
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
                    FirstName = dr["first_name"].ToString(),
                    LastName = dr["last_name"].ToString(),
                    RoomId = (int)dr["roomID"],
                    TelephoneNumber = dr["telephone_phoneNumber"].ToString(),
                    Age = (int)dr["age"],                };
                teachers.Add(teacher);
            }
            return teachers;
        }
    }
}