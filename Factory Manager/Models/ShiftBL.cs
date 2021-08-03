using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Manager.Models
{
    public class ShiftBL
    {
        factoryDBEntities db = new factoryDBEntities();

        public List<Shift> GetShifts()
        {
            return db.Shifts.ToList();
        }
        public List<NewShiftObj> GetNewShiftObjs()
        {
            var NewShiftList = from shift in db.Shifts
                              
                               
                               select new NewShiftObj
                               {
                                   ID = shift.ID,
                                   ShiftDate = shift.ShiftDate.ToString(),
                                   StartTime = (int)shift.StartTime,
                                   EndTime = (int)shift.EndTime,
                                   
                               };
            return NewShiftList.ToList();
        }
        public List<Employee> GetEmployeesInShift(int shiftID )
        {
            var empShifts = db.EmpoloyeeShifts.Where(x => x.ShiftID == shiftID).ToList();

            var EmpsInShift = new List<Employee>();

            foreach(var empShift in empShifts)
            {
                var chosenEmployee = db.Employees.Where(x => x.ID == empShift.EmployeeID).First();
                EmpsInShift.Add(chosenEmployee);
            }

            return EmpsInShift;

        }

        public void AddShift(Shift newShift)
        {
            db.Shifts.Add(newShift);
            db.SaveChanges();
        }
    }
}
