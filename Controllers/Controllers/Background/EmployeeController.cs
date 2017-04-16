using System;
using System.IO;
using System.Web;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;

using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using JNyuluSoft.Util;
using Newtonsoft.Json;
using System.Web.Security;

namespace JNyuluSoft.Web.Controllers
{
    /* ������Ϣ¼�� */
    [ControllerDetails(Area = "Background")]
    public class EmployeeController : DefaultController
    {
        protected readonly RegistrationService _registrationService = RegistrationService.GetInstance();
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();

        protected readonly GradeService _gradeService = GradeService.GetInstance();

        public void List(string searchTrueName, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                SearchTrueName = searchTrueName,
                PageIndex = pageIndex
            };
        }

        public void XHrList(string searchTrueName, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;

            PropertyBag["params"] = new
            {
                SearchTrueName = searchTrueName,
                PageIndex = pageIndex
            };

            int _totalNums = 0, _pageSize = 12;
            string _conAttr = "UserType = 0";

            if (!string.IsNullOrEmpty(searchTrueName))
            {
                _conAttr += " AND trueName LIKE '%" + searchTrueName + "%'";
            }

            IList<Employee> _employeeList = _employeeService.PaginatedEmployee(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            string uri = "javascript:jsPaginated('" + searchTrueName + "',{0});";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["employeeList"] = _employeeList;
            PropertyBag["ltlShowPager"] = ltlShowPager;

            CancelLayout();
        }

        public void Create(string searchTrueName, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                SearchTrueName = searchTrueName,
                PageIndex = pageIndex
            };
        }

        //["trueName","phone","lgID","schoolFee","remark"]
        public void PostCreate(string name, string trueName, string phone, string mobile, string remark)
        {
            int _code = 0;
            string _message = "��ͬѧע��ʧ��";
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(trueName))
            {
                Employee _employee = new Employee();
                _employee.UserType = 0;
                _employee.Name = name;

                _employee = _employeeService.GetEmployeeByName(_employee.Name);
                if (_employee == null)
                {
                    //YLS+ѧ�������ɵ�����+5λ���к� _employee.ID.ToString().PadLeft(5, '0')
                    //_employee.Name = "YLS" + _grade.SN + DateTime.Now.ToString("yyyyMMddHHmmss");
                    _employee = new Employee();
                    _employee.UserType = 0;
                    _employee.Name = name;

                    _employee.TrueName = trueName;
                    _employee.Phone = phone;
                    _employee.Mobile = mobile;
                    _employee.PassWord = mobile; //��ʼ������Ϊ�û����ֻ���
                    _employee.Permission = 0;
                    _employee.Description = remark;

                    _employee.ID = _employeeService.InsertEmployee(_employee);
                    _code = _employee.ID;

                    if (_code > 0)
                    {
                        _message = "�½�ѧԱ��Ϣ�ɹ�";
                    }
                }
                else
                {
                    _message = "��ͬѧ�Ѿ�ע��";
                }
            }
            else
            {
                _message = "ѧԱ��ѧ���ż���������Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int employeeID, string searchTrueName, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                SearchTrueName = searchTrueName,
                PageIndex = pageIndex
            };

            Employee _employee = _employeeService.GetEmployeeByID(employeeID);
            PropertyBag["employee"] = _employee;
        }

        public void PostEdit(int employeeID, string trueName, string phone,string mobile, string description)
        {
            int _code = 0;
            string _message = "�޸�ѧԱѧ����Ϣʧ��";
            if (!string.IsNullOrEmpty(trueName))
            {
                Employee _employee = _employeeService.GetEmployeeByID(employeeID);
                _employee.TrueName = trueName;
                _employee.Phone = phone;
                _employee.Mobile = mobile;
                _employee.Description = description;

                _code = _employeeService.UpdateEmployee(_employee);

                if (_code > 0)
                {
                    _message = "�޸�ѧԱѧ����Ϣ�ɹ�";
                }
            }
            else
            {
                _message = "ѧԱ��������Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostStatus(int employeeId, int locked)
        {
            Result _result = new Result
            {
                Code = -2,
                Data = "δ֪����"
            };
            Employee _employee = _employeeService.GetEmployeeByID(employeeId);
            if (_employee != null)
            {
                _employee.Locked = locked;
            }
            _employeeService.UpdateEmployee(_employee);
            _result.Code = locked;

            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);
            RenderText(_jsonString);
            CancelLayout();
        }

        public void ChkUserName(string userName)
        {
            Employee _employee = _employeeService.GetEmployeeByName(userName);
            if (_employee != null)
            {
                RenderText("1");
            }
            else
            {
                RenderText("0");
            }
            CancelView();
        }

        public void ImportExcel(string searchTrueName, int pageIndex)
        {
            string ASPSESSID = HttpContext.Session.SessionID;
            string AUTHID = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName] == null ? string.Empty : HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value;

            PropertyBag["params"] = new
            {
                SearchTrueName = searchTrueName,
                PageIndex = pageIndex,
                ASPSESSID = ASPSESSID,
                AuthID = AUTHID,
                FormsCookieName = FormsAuthentication.FormsCookieName,
            };
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void PostImportExcel()
        {
            string _fileExt = string.Empty;
            int _code = 0;
            string _message = string.Empty;

            try
            {
                HttpPostedFile _uploadFiles = Request.Files["uploadFile"] as HttpPostedFile;

                if (_uploadFiles == null || _uploadFiles.FileName.Length < 3)
                {
                    _message = "��û��ѡ��Ҫ�ϴ����ļ���";
                    _code = -4;
                }
                else
                {
                    _fileExt = Path.GetExtension(_uploadFiles.FileName).ToLower();//��ȡ�ļ���չ��
                    String[] _allowedExt = { ".xls", ".xlsx" }; //�����ϴ����ļ���ʽ
                    if (!_allowedExt.Contains(_fileExt))
                    {
                        _message = "���ϴ��ĸ�ʽΪ" + _fileExt + ", ��ͳֻ������չ��Ϊ" + string.Join("��", _allowedExt) + "��ʽ�ļ�";
                        _code = -3;
                    }
                    else if (_uploadFiles.ContentLength == 0)
                    {
                        _message = "�ϴ�������Ϊ�գ�";
                        _code = -2;
                    }
                    else if (_uploadFiles.ContentLength > 1024 * 1024 * 8)
                    {
                        _message = "���ϴ����ļ�������8M��";
                        _code = -1;
                    }
                }

                if (_code == 0)
                {
                    string _fileName = ShortFileNameUtil.ShortFileName(Guid.NewGuid().ToString()) + _fileExt;
                    string _virtualPath = "/UploadFiles/employeeXls/";

                    string _physicalPath = HttpContext.Server.MapPath(_virtualPath);
                    if (!Directory.Exists(_physicalPath))
                    {
                        Directory.CreateDirectory(_physicalPath);
                    }
                    string _fullPath = _physicalPath + _fileName;
                    _uploadFiles.SaveAs(_fullPath);

                    DataTable _employeeData = JNyuluUtils.ImportExcel(_fullPath);
                    int _i = 0;

                    StringBuilder _result = new StringBuilder();
                    _result.AppendLine("�ϴ�·��Ϊ��" + _virtualPath + _fileName);
                    _result.AppendLine("Excel������־��¼��");
                    foreach (DataRow dr in _employeeData.Rows)
                    {
                        _i++;
                        try
                        {
                            //["ѧ����"] dr["ѧ������"] dr["�ҳ��ֻ�"] dr["��ͥ�绰"] dr["��ע"]
                            string _name = dr["ѧ����"].ToString() + "";
                            if (!string.IsNullOrEmpty(_name))
                            {
                                Employee _employee = _employeeService.GetEmployeeByName(_name);
                                if (_employee == null)
                                {
                                    _employee = new Employee();

                                    _employee.UserType = 0;
                                    _employee.Name = _name;
                                    _employee.TrueName = dr["ѧ������"].ToString() + "";

                                    string _phone = dr["��ͥ�绰"].ToString() + "";
                                    if (_phone.IndexOf("e+") > 0)
                                    {
                                        _phone = (double.Parse(_phone)).ToString();
                                    }
                                    _employee.Phone = _phone;

                                    LogHelper.Info(dr["�ҳ��ֻ�"].GetType().ToString() + "  " + dr["�ҳ��ֻ�"]);

                                    string _mobile = dr["�ҳ��ֻ�"].ToString() + "";
                                    if (_mobile.IndexOf("e+") > 0)
                                    {
                                        _mobile = (double.Parse(_mobile)).ToString();
                                    }
                                    _employee.Mobile = _mobile;
                                    _employee.PassWord = _mobile; //��ʼ������Ϊ�û����ֻ���
                                    _employee.Permission = 0;
                                    _employee.ID = _employeeService.InsertEmployee(_employee);
                                }
                            }
                            else
                            {
                                LogHelper.Error("��¼[" + _i + "] �е�ѧ���Ų���Ϊ��");
                                _result.AppendLine("��¼[" + _i + "] �е�ѧ���Ų���Ϊ��");
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error("��¼[" + _i + "]������� " + ex.ToString());
                            _result.AppendLine("��¼[" + _i + "]������� " + ex.ToString());
                            continue;
                        }
                        _i++;
                    }

                    _message = _result.ToString();
                }
            }
            catch (Exception ex)
            {
                _code = -1;
                _message = ex.Message;
            }
            finally
            {
                RenderText("{code: " + _code + ",message: \"" + _message.Replace(@"\", @"\\") + "\"}");

                CancelLayout();
            }
        }

        public void PostDelete(int employeeID)
        {
            int _code = 0;
            string _message = "ɾ��ѧԱʧ��";

            _code = _employeeService.DeleteEmployeeByID(employeeID);

            if (_code > 0)
            {
                _message = "ɾ��ѧԱ�ɹ�";
            }
            else
            {
                _message = "�Բ���ɾ��ʧ�ܣ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostBatchDelete(string employeeIDList)
        {
            int _code = 0;
            string _message = "ɾ��ѧԱʧ��";

            if (!string.IsNullOrEmpty(employeeIDList))
            {
                string[] _employeeIDList = employeeIDList.Split(',');
                for (int i = 0; i < _employeeIDList.Length; i++)
                {
                    int _employeeID = 0;
                    int.TryParse(_employeeIDList[i], out _employeeID);

                    if (_employeeID > 0)
                    {
                        _code = _employeeService.DeleteEmployeeByID(_employeeID);
                        if (_code > 0)
                            _message = "ɾ��ѧԱ�ɹ�";
                    }
                }
            }
            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostAllDelete()
        {
            int _totalNum = 0;
            int _code = 0;
            string _message = "ɾ��ѧԱʧ��";

            IList<Employee> _employeeList = _employeeService.PaginatedEmployee(1, 10000, "ID", " ID <> 578", ref _totalNum);
            foreach (Employee employee in _employeeList)
            {
                _code = _employeeService.DeleteEmployeeByID(employee.ID);
                if (_code > 0)
                    _message = "ɾ��ѧԱ�ɹ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

    }
}
