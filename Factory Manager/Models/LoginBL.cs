using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Manager.Models
{
    public class LoginBL
    {
        factoryDBEntities db = new factoryDBEntities();

        public bool isAuthenticated(string userName, string pwd)
        {
            var result = db.Users.Where(x => x.UserName == userName && x.PassWrd == pwd);
            if(result.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int GetNumOfActions(string userPwd)
        {
            var selectedUser = db.Users.Where(x => x.PassWrd == userPwd).First();
            return selectedUser.NumOfActions.Value;
        }
        public string GetFullName(string userPwd)
        {
            var selectedUser = db.Users.Where(x => x.PassWrd == userPwd).First();
            return selectedUser.FullName.ToString();
        }
    }
}