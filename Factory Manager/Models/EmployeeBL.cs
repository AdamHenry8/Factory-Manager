using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Manager.Models
{
    public class EmployeeBL
    {
        factoryDBEntities db = new factoryDBEntities();

        

        public List<Employee> GetEmployees()
        {
            var selected = db.Employees.ToList();
            return selected;
        }

        public Employee GetEmployee(int id)
        {
           return db.Employees.Where(x => x.ID == id).First();
        }
        public List<NewEmpObj> GetNewEmpObjs()
        {
            var newEmployeeObj = from emp in db.Employees
                                 join depart in db.Departments on
                                 emp.Department equals depart.ID                             
                                 select new NewEmpObj
                                 {
                                     ID = emp.ID,
                                     FirstName = emp.FirstName,
                                     LastName = emp.LastName,
                                     StartWorkYear = (int)emp.StartWorkYear,
                                     DepartmentName = depart.DepartmentName,
                                     
                                 };

            return newEmployeeObj.ToList();
        }
        public List<EmployeeShiftsObj> GetEmployeeShiftDetails()
        {
            var NewEmpObj = from emp in db.Employees
                            join empShift in db.EmpoloyeeShifts on
                            emp.ID equals empShift.EmployeeID
                            join shift in db.Shifts on
                            empShift.ShiftID equals shift.ID
                            select new EmployeeShiftsObj
                            {
                                employeeID = emp.ID,
                                shiftDate = shift.ShiftDate.ToString(),
                                shiftHours = shift.StartTime.ToString() + " - " + shift.EndTime
                            };
            return NewEmpObj.ToList();
        }

        public void Edit(Employee editedEmployee)
        {
            var selectedEmployee = db.Employees.Where(x => x.ID == editedEmployee.ID).First();
            selectedEmployee.FirstName = editedEmployee.FirstName;
            selectedEmployee.LastName = editedEmployee.LastName;
            selectedEmployee.Department = editedEmployee.Department;
            selectedEmployee.StartWorkYear = editedEmployee.StartWorkYear;

            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var selected = db.Employees.Where(x => x.ID == id).First();
            db.Employees.Remove(selected);

            var selectedShifts = db.EmpoloyeeShifts.Where(x => x.EmployeeID == id).ToList();

            foreach(var shift in selectedShifts)
            {
                db.EmpoloyeeShifts.Remove(shift);
            }
            db.SaveChanges();
        }
        public EmpoloyeeShift GetEmployeeShifts(int id)
        {
           return db.EmpoloyeeShifts.Where(x => x.EmployeeID == id).First();
        }
        public void AddEmployeeShift(EmpoloyeeShift newEmployeeShift)
        {
            db.EmpoloyeeShifts.Add(newEmployeeShift);
            db.SaveChanges();
        }
        public List<Employee> Search(string searchPhrase)
        {
            return db.Employees.Where(x => x.FirstName.Contains(searchPhrase) || x.LastName.Contains(searchPhrase)).ToList();
        }



    }
}