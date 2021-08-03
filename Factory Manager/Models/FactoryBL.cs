using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Manager.Models
{
    public class FactoryBL
    {
        factoryDBEntities db = new factoryDBEntities();

        public List<MultipleClass> GetFactoryDB()
        {
            var multipleClassObj = from emp in db.Employees
                                   join depart in db.Departments on
                                   emp.Department equals depart.ID
                                   from empShift in db.EmpoloyeeShifts 
                                   join shift in db.Shifts on
                                   empShift.ShiftID equals shift.ID 
                                   
                                   select new MultipleClass
                                   {
                                       DepartmentDetails = depart,
                                       EmployeeDetails = emp,
                                       ShiftDetails = shift,
                                       EmpoloyeeShiftDetails = empShift
                                   };
            return multipleClassObj.ToList();
        }
    }
}