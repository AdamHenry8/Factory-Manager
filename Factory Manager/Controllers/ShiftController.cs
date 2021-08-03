using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory_Manager.Models;

namespace Factory_Manager.Controllers
{
    public class ShiftController : Controller
    {
        ShiftBL ShiftUtils = new ShiftBL();
        // GET: Shift
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShiftList()
        {
            if (Session["authenticated"] != null && (int)Session["numOfActs"] > 0)
            {
                var ShiftList = ShiftUtils.GetNewShiftObjs();

                foreach (var shift in ShiftList)
                {
                    var shiftEmployees = ShiftUtils.GetEmployeesInShift(shift.ID);
                    shift.shiftEmployees = shiftEmployees;
                }

                ViewBag.shifts = ShiftList;
                #region decrement user's actions
                // Get loged in user's num of actions
                int NumOfActs = (int)Session["numOfActs"];
                ViewBag.NumOfActs = NumOfActs;
                //Decrement it's value and save the new value in the session
                NumOfActs -= 1;
                #endregion

                return View("ShiftList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult AddShift()
        {
            return View("AddShift");
        }
        public ActionResult GetShiftsListWithAddedShift(Shift newShift)
        {

            if ((int)Session["numOfActs"] > 0)
            {

                ShiftUtils.AddShift(newShift);
                return RedirectToAction("ShiftList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}