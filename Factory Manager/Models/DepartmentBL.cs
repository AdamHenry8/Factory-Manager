using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Manager.Models
{
    public class DepartmentBL
    {
        factoryDBEntities db = new factoryDBEntities();

        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();

        }
        public Department GetDepartment(int id)
        {
            return db.Departments.Where(x => x.ID == id).First();
        }

        public void AddDepartment(Department newDepartment)
        {
            db.Departments.Add(newDepartment);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var selected = db.Departments.Where(x => x.ID == id).First();
            db.Departments.Remove(selected);
            db.SaveChanges();
        }
        public void Edit(Department editedDepartment)
        {
            var selected = db.Departments.Where(x => x.ID == editedDepartment.ID).First();
            selected.DepartmentName = editedDepartment.DepartmentName;
            selected.Manager = editedDepartment.Manager;
            db.SaveChanges();
        }
        public List<newDepartObj> GetNewDepartObjs()

        {
            var selected = from depart in db.Departments
                           join emp in db.Employees
                           on depart.Manager equals emp.ID
                           

                           select new newDepartObj
                           {
                               ID = depart.ID,
                               DepartmentName = depart.DepartmentName,
                               Manager = emp.FirstName   

                           };
            


            return selected.ToList();
        }
        public bool isDepartmentEmpty(int DepartmentID)
        {
           var selected = db.Employees.Where(x => x.Department == DepartmentID).FirstOrDefault();
            if(selected != null && selected.Department == DepartmentID)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}