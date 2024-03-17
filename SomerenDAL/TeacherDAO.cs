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
            string query = "SELECT first_name, last_name, age, telephone_number, roomID FROM [LECTURER]";
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
                    Age = (int)dr["age"],
                    TelephoneNumber = dr["telephone_number"].ToString(),
                    RoomId = (int)dr["roomID"]
                };
                teachers.Add(teacher);
            }
            return teachers;
        }
    }
}