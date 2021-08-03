using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Manager.Models
{
    public class NewEmpObj
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StartWorkYear { get; set; }
        public string DepartmentName { get; set; }
        public List<EmployeeShiftsObj> EmployeeShifts { get; set; }
    }
}