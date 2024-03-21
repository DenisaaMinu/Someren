namespace SomerenModel
{
    public class Teacher
    {
        public int Id { get; set; }     // database id
        public int RoomId { get; set; } // database roomId
        public string FirstName { get; set; }  // TeacherFirstName, e.g. Denisa 
        public string LastName { get; set; }  // TeacherLastName, e.g. Minu
        public int Age { get; set; }  // TeacherAge, e.g. 40
        public string TelephoneNumber { get; set; }   //TeacherPhoneNumber, e.g. 0583718203
    }
}