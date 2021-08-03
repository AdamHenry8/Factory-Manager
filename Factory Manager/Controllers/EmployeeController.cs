using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory_Manager.Models;

namespace Factory_Manager.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeBL EmployeeUtils = new EmployeeBL();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EmployeeList()
        {
            if(Session["authenticated"] != null)
            {
                //Get EmployeeList (combined with department)
                var EmployeeList = EmployeeUtils.GetNewEmpObjs();
                ViewBag.employeeList = EmployeeList;

                //Get Employee Shifts
                var EmployeeShiftDetailsList = EmployeeUtils.GetEmployeeShiftDetails();

                foreach(var employee in EmployeeList)
                {
                    employee.EmployeeShifts = EmployeeShiftDetailsList.Where(x => x.employeeID == employee.ID).ToList();    
                }

                #region decrement user's actions
                // Get loged in user's num of actions
                int NumOfActs = (int)Session["numOfActs"];
                ViewBag.NumOfActs = NumOfActs;
                //Decrement it's value and save the new value in the session
                NumOfActs -= 1;
                #endregion

                return View("EmployeeList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        public ActionResult EditEmployee(int id)
        {
            if (Session["authenticated"] != null)
            {
                var selectedEmployee = EmployeeUtils.GetEmployee(id);
                return View("EditEmployee", selectedEmployee);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        public ActionResult GetEditedEmployeeList(Employee editedEmployee)
        {
            if ((int)Session["numOfActs"] > 0)
            {
                EmployeeUtils.Edit(editedEmployee);
                return RedirectToAction("EmployeeList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        public ActionResult DeleteEmployee(int id)
        {
            if ((int)Session["numOfActs"] > 0)
            {
                EmployeeUtils.Delete(id);
                return RedirectToAction("EmployeeList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult AddShift(int id)
        {
            if (Session["authenticated"] != null)
            {
                var selected = EmployeeUtils.GetEmployeeShifts(id);
                return View("AddShiftToEmployee", selected);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
        public ActionResult GetListWithAddedShifts(EmpoloyeeShift newEmpShift)
        {
            if ((int)Session["numOfActs"] > 0)
            {

                EmployeeUtils.AddEmployeeShift(newEmpShift);
                return RedirectToAction("EmployeeList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    
        public ActionResult SearchResults(string searchPhrase)
        {
            if ((int)Session["numOfActs"] > 0)
            {
                var searchResults = EmployeeUtils.Search(searchPhrase);
                ViewBag.searchResults = searchResults;

                #region decrement user's actions
                // Get loged in user's num of actions
                int NumOfActs = (int)Session["numOfActs"];
                ViewBag.NumOfActs = NumOfActs;
                //Decrement it's value and save the new value in the session
                NumOfActs -= 1;
                #endregion
                return View("SearchEmployee");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        public ActionResult LogOut()
        {
            return RedirectToAction("Index", "Login");
        }


     
    }
}