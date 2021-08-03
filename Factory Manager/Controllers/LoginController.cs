using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory_Manager.Models;

namespace Factory_Manager.Controllers
{
    public class LoginController : Controller
    {
        LoginBL LoginUtils = new LoginBL();
        // GET: Login
        public ActionResult Index()
        {
            Session["authenticated"] = null;           
            return View("LoginPage");  
        }
        [HttpPost]
        public ActionResult AuthenticateLogin(string userName, string pwd)
        {
            bool isAuthenticated = LoginUtils.isAuthenticated(userName, pwd);
            if(isAuthenticated == true)
            {
                //get full name for loged in user
                var userFullName = LoginUtils.GetFullName(pwd);
                Session["fullName"] = userFullName;
                //get number of actions for loged in user
                var userNumOfActions = LoginUtils.GetNumOfActions(pwd);
                Session["numOfActs"] = userNumOfActions;
                //set authenticateion value
                Session["authenticated"] = true;
                return RedirectToAction("HomePage", "Home");
            }
            return RedirectToAction("Index");
        }
       
        
    
    }
}