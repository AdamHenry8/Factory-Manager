using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Manager.Models
{
    public class NewShiftObj
    {
        public int ID { get; set; }
        public string ShiftDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public List<Employee> shiftEmployees { get; set; }
    }
}