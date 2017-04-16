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

namespace JNyuluSoft.Web.Controllers
{
    /* Ȩ�� - ���� */
    [ControllerDetails(Area = "Background")]
    public class SecurityController : DefaultController 
    {
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();

        public void List(int pageIndex)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
 
            int _totalNums = 0, _pageSize = 12;
            string _conAttr = "UserType = 2";

            IList<Employee> _emplyeeList = _employeeService.PaginatedEmployee(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            PropertyBag["emplyeeList"] = _emplyeeList;
        }

        public void Create()
        { }

        public void PostCreate(string name, string passWord, string description)
        {
            int _code = 0;
            string _message = "�����û���Ϣʧ��";
            if (!string.IsNullOrEmpty(name))
            {
                Employee _employee = new Employee();
                _employee.UserType = 2;
                _employee.Name = name;
                _employee.PassWord = passWord;
                _employee.Permission = 0; 
                _employee.Description = description;
                _code = _employeeService.InsertEmployee(_employee);

                if (_code > 0)
                {
                    _message = "�����ʺųɹ�";
                }
                else
                {
                    _message = "�Բ���ѧ���ű�ռ�ã�";
                }
            }
            else
            {
                _message = "ѧ���Ų���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int employeeID)
        {
            Employee _employee = _employeeService.GetEmployeeByID(employeeID);
            PropertyBag["employee"] = _employee;
        }

        public void PostEdit(int employeeID, int permission)
        {
            int _code = 0;
            string _message = "�޸�Ȩ����Ϣʧ��";

            Employee _employee = _employeeService.GetEmployeeByID(employeeID);
            _employee.Permission = permission; 

            _code = _employeeService.UpdateEmployee(_employee);

            if (_code > 0)
            {
                _message = "�޸�Ȩ����Ϣ�ɹ�";
            }
            else
            {
                _message = "�Բ����޸�ʧ�ܣ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int employeeID)
        {
            int _code = 0;
            string _message = "ɾ���ʺ�ʧ��";

            _code = _employeeService.DeleteEmployeeByID(employeeID);

            if (_code > 0)
            {
                _message = "ɾ���ʺųɹ�";
            }
            else
            {
                _message = "�Բ����޸�ʧ�ܣ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
