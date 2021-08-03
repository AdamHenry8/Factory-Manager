using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Manager.Models
{
    public class MultipleClass
    {
        public Department DepartmentDetails { get; set; }
        public Employee EmployeeDetails { get; set; }
        public Shift ShiftDetails { get; set; }
        public EmpoloyeeShift EmpoloyeeShiftDetails { get; set; }

    }
}