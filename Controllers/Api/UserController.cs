using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MonoRail.Framework;

using Newtonsoft.Json;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using System.Collections;

namespace JNyuluSoft.Web.Api.Controllers
{
    [ControllerDetails(Area = "Api")]
    public class UserController : ApiController
    {
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();
        protected readonly GradeService _gradeService = GradeService.GetInstance();

        /// <summary>
        /// 用户帐号验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        public void CheckingLogin(string userName, string passWord)
        {
            /*
             * 用户验证
            */
            int _employeeID = _employeeService.VerifyEmployee(userName, passWord);
            if (_employeeID > 0)
            {
                _result.Code = 0;
                Employee _employee = _employeeService.GetEmployeeByID(_employeeID);

                _employee.LastLoginDate = _employee.LoginDate;
                _employee.LoginDate = DateTime.Now;
                _employeeService.UpdateEmployee(_employee);

                _result.Data = _employee;
            }
            else {
                _result.Code = -1;
                _result.Data = "对不起,帐号不存在";
            }

            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="newPwd"></param>
        public void ResetPwd(string userName, string passWord, string newPwd)
        {
            int _employeeID = _employeeService.VerifyEmployee(userName, passWord);
            if (_employeeID > 0)
            {
                Employee _employee = _employeeService.GetEmployeeByName(userName);

                _employee.PassWord = newPwd;
                _result.Code = _employeeService.UpdateEmployee(_employee);

                if (_result.Code > 0)
                {
                    _result.Data = _employee;
                }
                else
                {
                    _result.Data = "对不起，密码修改失败";
                }
            }
            else
            {
                _result.Code = -1;
                _result.Data = "对不起,帐号不存在";
            }

            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }
    }
}