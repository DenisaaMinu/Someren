using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;
using System;

namespace SomerenDAL
{
    public class StudentDao : BaseDao
    {
        public List<Student> GetAllStudents()
        {
            string query = "SELECT studentId, roomID, studentNumber, name, class, phoneNumber FROM [STUDENT]";

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
                    Id = (int)dr["studentId"],
                    RoomId = (int)dr["roomID"],
                    Number = dr["studentNumber"].ToString(),
                    Name = dr["name"].ToString(),
                    Class = dr["class"].ToString(),
                    PhoneNumber = dr["phoneNumber"].ToString(),
                };
                students.Add(student);
            }
            return students;
        }

        public Student AddStudent(Student student)
        {
            try
            {
                string query = "INSERT INTO [STUDENT] (roomID, studentNumber, name, phoneNumber, class) " +
                               "VALUES (@RoomId, @Number, @Name, @PhoneNumber, @Class);";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@RoomId", student.RoomId),
                    new SqlParameter("@Number", student.Number),
                    new SqlParameter("@Name", student.Name),
                    new SqlParameter("@PhoneNumber", student.PhoneNumber),
                    new SqlParameter("@Class", student.Class)
                };

                ExecuteEditQuery(query, parameters);
                return student;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteStudent(Student student)
        {
            string query = "DELETE FROM [STUDENT] WHERE studentId = @Id";

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Id", student.Id)
            };

            ExecuteEditQuery(query, sqlParameters);
        }

        public void ModifyStudent(Student student)
        {
            string query = "UPDATE [STUDENT] SET studentNumber = @Number, [name] = @Name, phoneNumber = @PhoneNumber, class = @Class " +
                           "WHERE studentId = @Id;";

            SqlParameter[] sqlParameters =
            {
                 new SqlParameter("@Number", student.Number),
                 new SqlParameter("@Name", student.Name),
                 new SqlParameter("@PhoneNumber", student.PhoneNumber),
                 new SqlParameter("@Class", student.Class),
                 new SqlParameter("@Id", student.Id)
            };

            ExecuteEditQuery(query, sqlParameters);
        }

        // Method to fetch participating students by activity number
        public List<Student> GetParticipants(int activityNumber)
        {
            // SQL query to select all students associated with the provided activity number
            string query = "SELECT * FROM STUDENT s " +
                           "JOIN PARTICIPANTS p ON s.[studentId] = p.[studentId] " +
                           "WHERE p.[activityId] = @activityId";

            // Parameters for the query
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@activityId", SqlDbType.Int);
            sqlParameters[0].Value = activityNumber;

            // Execute the query and retrieve data
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            // Convert the DataTable to a list of Student objects
            return ReadTables(dataTable);
        }
    }
}