using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;

namespace SomerenDAL
{
    public class StudentDao : BaseDao
    {
        public List<Student> GetAllStudents()
        {
            string query = "SELECT student_id, studentNumber, name, class, phoneNumber FROM [STUDENT]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Student> ReadTables(DataTable dataTable)
        {
            List<Student> students = new List<Student>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Student student = new Student()
                {
                    Id = (int)dr["student_id"],
                    Number = dr["studentNumber"].ToString(),
                    Name = dr["name"].ToString(),
                    Class = dr["class"].ToString(),
                    TelephoneNumber = dr["phoneNumber"].ToString(),
                };
                students.Add(student);
            }
            return students;
        }
    }
}