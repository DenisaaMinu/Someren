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
            string query = "SELECT building, roomNumber, roomsize, roomcapacity, roomtype FROM [ROOM]";
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
                    Building = dr["building"].ToString(),
                    Number = (int)dr["roomnumber"],
                    Size = (int)dr["roomsize"],
                    Capacity = (int)dr["roomcapacity"],
                    Type = dr["roomtype"].ToString()
                };
                rooms.Add(room);
            }
            return rooms;
        }
    }
}