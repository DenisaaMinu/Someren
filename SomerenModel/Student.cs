using System;

namespace SomerenModel
{
    public class Student
    {
        public int Id { get; set; }     // database id
        public string Name { get; set; }  // StudentName, e.g. Brian van den Berg
        public string Number { get; set; } // StudentNumber, e.g. 474791
        public string Class {  get; set; }  // StudentClass, e.g. IT1C
        public string TelephoneNumber { get; set; }  // Phone number, e.g. 0593482485
    }
}