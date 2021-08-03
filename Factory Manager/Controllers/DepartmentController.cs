using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory_Manager.Models;

namespace Factory_Manager.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentBL DepartmentUtils = new DepartmentBL();
        EmployeeBL EmployeeUtils = new EmployeeBL();

        FactoryBL FactoryUtils = new FactoryBL();

        
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult DepartmentList()
        {
            if(Session["authenticated"] != null && (int)Session["numOfActs"] > 0)
            {
                var DepartList = DepartmentUtils.GetNewDepartObjs();
                ViewBag.departments = DepartList;


                foreach (var department in DepartList)
                {
                    var result = DepartmentUtils.isDepartmentEmpty(department.ID);
                    department.isDepartmentEmpty = result;
                }
                // Get loged in user's num of actions
                int NumOfActs = (int)Session["numOfActs"];
                ViewBag.NumOfActs = NumOfActs;
                //Decrement it's value and save the new value in the session
                NumOfActs -= 1;
                Session["numOfActs"] = NumOfActs;
                

                return View("DepartmentList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
          
        }
        public ActionResult AddDepartment()
        {
            if (Session["authenticated"] != null)
            {
                return View("AddDepartment");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
        public ActionResult GetListWithAddedDepartment(Department NewDepartment)
        {
            if((int)Session["numOfActs"] > 0)
            {
                DepartmentUtils.AddDepartment(NewDepartment);
                return RedirectToAction("DepartmentList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           

        }
        public ActionResult DeleteDepartment(int id)
        {
            if ((int)Session["numOfActs"] > 0)
            {
                    DepartmentUtils.Delete(id);
                    return RedirectToAction("DepartmentList");
              }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult EditDepartment(int id)
        {
            if (Session["authenticated"] != null)
            {
                var selectedDepartment = DepartmentUtils.GetDepartment(id);
                return View("EditDepartment", selectedDepartment);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
              
        }
        public ActionResult GetEditedDeparmentList(Department EditedDepartment)
        {
            if ((int)Session["numOfActs"] > 0)
            {
                DepartmentUtils.Edit(EditedDepartment);
                return RedirectToAction("DepartmentList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
      
       
    }


    }
