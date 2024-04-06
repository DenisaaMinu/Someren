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
            string query = "SELECT roomId, building, roomNumber, roomSize, roomCapacity, roomType FROM [ROOM]";
           
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Room> ReadTables(DataTable dataTable)
        {
            List<Room> rooms = new List<Room>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Room room = new Room()
                {
                    Id = (int)dr["roomId"],
                    Building = dr["building"].ToString(),
                    Number = (int)dr["roomNumber"],
                    Size = (int)dr["roomSize"],
                    Capacity = (int)dr["roomCapacity"],
                    Type = dr["roomType"].ToString()
                };
                rooms.Add(room);
            }
            return rooms;
        }
    }
}