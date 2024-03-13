using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;

namespace SomerenDAL
{
    public class RoomDao : BaseDao
    {
        public List<Room> GetAllRooms()
        {
            string query = "SELECT roomNumber, roomType, roomCapacity FROM [ROOM]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Room> ReadTables(DataTable dataTable)
        {
            List<Room> rooms = new List<Room>();

            foreach (DataRow dr in dataTable.Rows)
            {
                string roomType = dr["roomType"].ToString();
                bool isTeacherRoom = (roomType == "room");

                Room room = new Room()
                {
                    Number = (int)dr["roomNumber"],
                    Type = !isTeacherRoom,
                    Capacity = (int)dr["roomCapacity"]
                };
                rooms.Add(room);
            }
            return rooms;
        }
    }
}