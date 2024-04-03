using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Activity
    { 
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LecturerId { get; set; }
        public string Name { get; set; }
    }
}
