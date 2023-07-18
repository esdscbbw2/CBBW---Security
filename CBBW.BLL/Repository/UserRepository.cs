using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BOL.CTV;
using CBBW.BOL.RBACUsers;
using CBBW.DAL.Entities;
using Newtonsoft.Json;

namespace CBBW.BLL.Repository
{
    public class UserRepository: IUserRepository
    {
        CTVEntities _CTVEntities;
        RBACUserEntities _RBACUserEntities;
        Stack<string> cburls;

        public UserRepository()
        {
            _CTVEntities = new CTVEntities();
            _RBACUserEntities = new RBACUserEntities();
        }
        public bool LogIn(string UserName, ref string pMsg)
        {
            UserInfo user= _CTVEntities.getLogInUserInfo(UserName, ref pMsg);
            if (user != null && user.EmployeeNumber > 0)
            {
                //user.IsOffline = user.EmployeeNumber == 2002563 ? false : user.IsOffline;
                user.Modules = GetUserModule(user.EmployeeNumber, user.CentreCode, ref pMsg);
                setUser(user);
                return true;
            }
            else 
            {
                return false;
            }
        }
        private void setUser(UserInfo user)
        {
            string json = JsonConvert.SerializeObject(user, Formatting.None);
            //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //var json = serializer.Serialize(user);
            HttpContext.Current.Response.Cookies.Add(
                new HttpCookie("LUser", json)
                { Expires = DateTime.Now.AddDays(7) });
        }
        public UserInfo getLoggedInUser()
        {
            UserInfo user = new UserInfo(true);
            HttpCookie cookie = HttpContext.Current.Request.Cookies["LUser"];
            if (cookie != null)
            {
                user = JsonConvert.DeserializeObject<UserInfo>(cookie.Value);
            }            
            return user;
        }
        public UserInfo getLoggedInUser(Controller controller)
        {
            UserInfo user = new UserInfo(true);
            HttpCookie cookie = HttpContext.Current.Request.Cookies["LUser"];
            if (cookie != null)
            {
                user = JsonConvert.DeserializeObject<UserInfo>(cookie.Value);
            }
            //SetUserMenu(controller,user.EmployeeNumber, user.CentreCode);
            SetUserMenu(controller, 38682, 13);
            return user;
        }
        private void setUserExpire(int days)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["LUser"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(days);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        public void LogOut()
        {
            //clearing cookies
            HttpCookie cookie = HttpContext.Current.Request.Cookies["LUser"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddMonths(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                ClearCallBackRecording();
            }
        }


        //User Navigation records
        private Stack<string> ReadCBUsFromCookie() 
        {
            cburls = new Stack<string>();
            HttpCookie cookie = HttpContext.Current.Request.Cookies["CBUR"];
            if (cookie != null)
            {
                cburls = JsonConvert.DeserializeObject<Stack<string>>(cookie.Value);
            }
            return cburls;
        }
        private void WriteCBUsCookie(Stack<string> urls)
        {
            string json = JsonConvert.SerializeObject(urls.Reverse(), Formatting.None);
            HttpContext.Current.Response.Cookies.Add(
                new HttpCookie("CBUR", json)
                { Expires = DateTime.Now.AddDays(7) });
        }
        public void RecordCallBack(string url)
        {           
            if (url != "")
            {
                cburls = ReadCBUsFromCookie();
                cburls.Push(url);
                WriteCBUsCookie(cburls);
            }


        }
        public string GetCallBackUrl() 
        {
            string url = "";
            cburls = ReadCBUsFromCookie();
            if (cburls.Count > 0)
            {
                url = cburls.Peek();
                cburls.Pop();
                WriteCBUsCookie(cburls);
            }
            return url;
        }
        public void ClearCallBackRecording()
        {
            //clearing cookies
            HttpCookie cookie = HttpContext.Current.Request.Cookies["CBUR"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddMonths(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
       
        // RBAC
        public List<UserMenu> GetUserMenu(int EmployeeNumber, int CentreCode,int ModuleID, ref string pMsg)
        {
            return _RBACUserEntities.GetUserMenu(EmployeeNumber, CentreCode, ModuleID, ref pMsg);
        }
        public List<UserModule> GetUserModule(int EmployeeNumber, int CentreCode, ref string pMsg)
        {
            return _RBACUserEntities.GetUserModule(EmployeeNumber, CentreCode, ref pMsg);
        }
        public void SetUserMenu(Controller controller,int EmployeeNumber,int CentreCode) 
        {
            string msg = "";
            List<UserMenu> x;
            if (controller.TempData["UserMenu"] != null) 
            {
                x= controller.TempData["UserMenu"] as List<UserMenu>;
            }
            else 
            {
                x = _RBACUserEntities.GetUserMenu(EmployeeNumber,CentreCode,0,ref msg);
            }
            controller.TempData["UserMenu"] = x;
        }



    }
}
