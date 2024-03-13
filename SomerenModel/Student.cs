using System;

namespace SomerenModel
{
    public class Student
    {
        public int Id { get; set; }     // database id
        public string Name { get; set; }
        public string Number { get; set; } // StudentNumber, e.g. 474791
        public string Class {  get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}