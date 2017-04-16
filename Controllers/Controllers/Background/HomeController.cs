using System;
using System.IO;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using JNyuluSoft.Util;

namespace JNyuluSoft.Web.Controllers
{
    [ControllerDetails(Area = "Background")]
    public class HomeController : DefaultController 
    {
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();
        protected readonly GradeService _gradeService = GradeService.GetInstance();

        public void Index()
        {
            Employee _employee = Session["logonUser"] as Employee;

            int _essential = _employee.Permission.Value & Convert.ToInt32(PermissionType.Essential, 2); //基本信息
            PropertyBag["essential"] = _essential;

            int _student = _employee.Permission.Value & Convert.ToInt32(PermissionType.Student, 2);  //学生管理 
            PropertyBag["student"] = _student;

            int _article = _employee.Permission.Value & Convert.ToInt32(PermissionType.Article, 2); //网站信息发布 
            PropertyBag["article"] = _article;

            CancelLayout();
        }

        public void Welcome()
        {

        }

        public void EditAccount()
        { 

        }

        public void PostEditAccount(string newPassWord)
        {
            int _code = 0;
            string _message = "密码设置失败";

            Employee _employee = Session["logonUser"] as Employee;
            if (_employee != null)
            {
                _employee = _employeeService.GetEmployeeByID(_employee.ID);
                _employee.PassWord = newPassWord;

                _code = _employeeService.UpdateEmployee(_employee);
                Session["logonUser"] = _employee;

                _message = "密码设置成功";
            }
            else
            {
                _message = "您没有权限设置密码";
            }
            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void Login()
        {
            int _totalNums = 0, _pageSize = 120, pageIndex = 1;
            string _conAttr = "1 = 1";

            IList<Grade> _gradeList = _gradeService.PaginatedGrade(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["gradeList"] = _gradeList;

        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void PostLogin(string userName, string passWord, int gradeID)
        {
          int _uid = 0;
          string _message = "对不起，您的帐号不存或已被冻结！";

          /*
           * 用户验证
          */
          _uid = _employeeService.VerifyEmployee(userName, passWord);
          if (_uid > 0)
          {
            Employee _employee = _employeeService.GetEmployeeByID(_uid);

            _employee.LastLoginDate = _employee.LoginDate;
            _employee.LoginDate = DateTime.Now;
            _employeeService.UpdateEmployee(_employee);

            if (_employee.UserType == 2)
            {
              _employee.GradeID = gradeID;
            }

            Session["logonUser"] = _employee;

            _message = userName + "：恭喜您登录成功！";
          }
          RenderText("{\"code\":\"" + _uid + "\",\"message\":\"" + _message + "\"}");

          CancelView();
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void Logout()
        {
            if (Session["logonUser"] != null)
            {
                Session.Remove("logonUser");
            }
            Response.Redirect("/Background/Home/Login.do");
            CancelView();
        }
    }
}
