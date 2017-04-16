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

namespace JNyuluSoft.Web.Controllers
{
    /* 报名信息录入 */
    [ControllerDetails(Area = "Background")]
    public class RegistrationController : DefaultController
    {
        protected readonly GradeService _gradeService = GradeService.GetInstance();

        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();

        protected readonly RegistrationService _registrationService = RegistrationService.GetInstance();
        protected readonly AssessmentService _assessmentService = AssessmentService.GetInstance();

        public void Create(string searchUserName, int stID, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                SearchUserName = searchUserName,
                STID = stID,
                PageIndex = pageIndex
            };

            Employee _logonUser = Session["logonUser"] as Employee;

            int _totalNums = 0;
            string _conAttr = "1 = 1";
            if (_logonUser.UserType == 2 && _logonUser.GradeID > 0)
                _conAttr = "Grade.ID = " + _logonUser.GradeID;

            IList<Grade> _gradeList = _gradeService.PaginatedGrade(1, 120, "ID", _conAttr, ref _totalNums);
            PropertyBag["gradeList"] = _gradeList;

            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;
        }

        //["trueName","phone","lgID","schoolFee","remark"]
        public void PostCreate(string employeeName, string schoolName, int gradeID, string gradeName, int stID, int schoolFee, string remark)
        {
            int _code = 0;
            string _message = "新增班级成员失败";
            if (!string.IsNullOrEmpty(employeeName))
            {
                Employee _employee = new Employee();
                _employee.UserType = 0;
                _employee.Name = employeeName;

                _employee = _employeeService.GetEmployeeByName(_employee.Name);
                if (_employee != null)
                {
                    if (_employee.ID > 0 && stID > 0)
                    {
                        Registration _registration = new Registration();
                        _registration.EmployeeID = _employee.ID;
                        _registration.SchoolName = schoolName;
                        _registration.GradeID = gradeID;
                        _registration.GradeName = gradeName;
                        _registration.STID = stID;
                        _registration.SchoolFee = schoolFee;
                        _registration.Remark = remark;

                        _code = _registrationService.InsertRegistration(_registration);
                        if (_code > 0)
                        {
                            _message = "新增班级成员成功";
                        }
                        else
                        {
                            _message = "此同学已经注册";
                        }
                    }
                }
                else
                {
                    _message = "学籍号不存在";
                }
            }
            else
            {
                _message = "学籍号不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }


        public void ImportExcel(string searchUserName, int stID, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                SearchUserName = searchUserName,
                STID = stID,
                PageIndex = pageIndex
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
                    _message = "您没有选择要上传的文件！";
                    _code = -4;
                }
                else
                {
                    _fileExt = Path.GetExtension(_uploadFiles.FileName).ToLower();//获取文件扩展名
                    String[] _allowedExt = { ".xls", ".xlsx" }; //允许上传的文件格式
                    if (!_allowedExt.Contains(_fileExt))
                    {
                        _message = "您上传的格式为" + _fileExt + ", 本统只接受扩展名为" + string.Join("、", _allowedExt) + "格式文件";
                        _code = -3;
                    }
                    else if (_uploadFiles.ContentLength == 0)
                    {
                        _message = "上传的内容为空！";
                        _code = -2;
                    }
                    else if (_uploadFiles.ContentLength > 1024 * 1024 * 8)
                    {
                        _message = "您上传的文件超过了8M！";
                        _code = -1;
                    }
                }

                if (_code == 0)
                {
                    string _fileName = ShortFileNameUtil.ShortFileName(Guid.NewGuid().ToString()) + _fileExt;
                    string _virtualPath = "/UploadFiles/registrationXls/";

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
                    _result.AppendLine("上传路径为：" + _virtualPath + _fileName);
                    _result.AppendLine("Excel解析日志记录：");
                    foreach (DataRow dr in _employeeData.Rows)
                    {
                        _i++;
                        try
                        {
                            //["学籍号"] ["学生姓名"] dr["报名时间"] dr["班级"] dr["原校"] dr["原来年级"] dr["金额"] dr["备注"]
                            string _name = dr["学籍号"].ToString() + "";
                            if (!string.IsNullOrEmpty(_name))
                            {
                                Employee _employee = _employeeService.GetEmployeeByName(_name);
                                if (_employee != null)
                                {
                                    string _gradeName = dr["原来年级"].ToString() + "";
                                    Grade _grade = _gradeService.GetGradeByName(_gradeName);
                                    if (_grade != null)
                                    {
                                        int _stID = 0;
                                        string _schoolTermName = dr["班级"].ToString();
                                        SchoolTerm _schoolTerm = _schoolTermService.GetBaseSchoolTermByName(_schoolTermName);
                                        if (_schoolTerm != null)
                                        {
                                            _stID = _schoolTerm.ID;
                                        }

                                        int _schoolFee = 0;
                                        int.TryParse(dr["金额"].ToString(), out _schoolFee);

                                        Registration _registration = new Registration();
                                        _registration.EmployeeID = _employee.ID;

                                        _registration.SchoolName = dr["原校"].ToString() + "";
                                        _registration.GradeID = _grade.ID;
                                        _registration.GradeName = _grade.Name;

                                        _registration.STID = _stID;
                                        _registration.SchoolFee = _schoolFee;
                                        _registration.Remark = dr["备注"].ToString() + "";

                                        DateTime _recordDate = DateTime.Today;
                                        if (DateTime.TryParse(dr["报名时间"].ToString() + "", out _recordDate))
                                        {
                                            _registration.RecordDate = _recordDate;
                                        }
                                        else
                                        {
                                            LogHelper.Info("记录[" + _i + "]考试时间格式错误");
                                            _result.AppendLine("记录[" + _i + "]考试时间格式错误");
                                            continue;
                                        }

                                        _code = _registrationService.InsertRegistration(_registration);
                                    }
                                    else
                                    {
                                        LogHelper.Error("记录[" + _i + "]原来年级" + _gradeName + "不存在");
                                        _result.AppendLine("记录[" + _i + "]原来年级" + _gradeName + "不存在");
                                    }
                                }
                                else
                                {
                                    LogHelper.Error("记录[" + _i + "]学籍号" + _name + "不存在，您必须选注册学籍号");
                                    _result.AppendLine("记录[" + _i + "]学籍号" + _name + "不存在，您必须选注册学籍号");
                                }
                            }
                            else
                            {
                                LogHelper.Error("记录[" + _i + "] 中的学籍号不能为空");
                                _result.AppendLine("记录[" + _i + "] 中的学籍号不能为空");
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error("记录[" + _i + "]导入出错 " + ex.ToString());
                            _result.AppendLine("记录[" + _i + "]导入出错 " + ex.ToString());
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
                RenderText("{code: " + _code + ",message: '" + _message + "'}");

                CancelLayout();
            }
        }

        public void Build(int employeeID, string searchTrueName, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                SearchTrueName = searchTrueName,
                PageIndex = pageIndex
            };

            Employee _logonUser = Session["logonUser"] as Employee;

            int _totalNums = 0, _pageSize = 12;
            string _conAttr = "1 = 1";
            if (_logonUser.UserType == 2 && _logonUser.GradeID > 0)
                _conAttr = "Grade.ID = " + _logonUser.GradeID;

            IList<Grade> _gradeList = _gradeService.PaginatedGrade(1, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["gradeList"] = _gradeList;

            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

            PropertyBag["employee"] = _employeeService.GetEmployeeByID(employeeID);
        }

        //"name","lgID","schoolFee","remark"
        public void PostBuild(string employeeName, string schoolName, int gradeID, string gradeName, int stID, int schoolFee, string remark)
        {
            int _code = 0;
            string _message = "为学员填写报名表失败";
            if (!string.IsNullOrEmpty(employeeName))
            {
                Employee _employee = _employeeService.GetEmployeeByName(employeeName);
                if (_employee != null)
                {
                    Registration _registration = null;

                    _registration = new Registration();
                    _registration.EmployeeID = _employee.ID;
                    _registration.SchoolName = schoolName;
                    _registration.GradeID = gradeID;
                    _registration.GradeName = gradeName;

                    _registration.STID = stID;
                    _registration.SchoolFee = schoolFee;
                    _registration.Remark = remark;
                    _code = _registrationService.InsertRegistration(_registration);

                    if (_code > 0)
                    {
                        _message = "为学员填写报名表成功";
                    }
                    else
                    {
                        _message = "此学员已经报名";
                    }
                }
                else
                {
                    _message = "此学籍号还没有在系统中登记";
                }
            }
            else
            {
                _message = "学籍号不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Main(string searchUserName, int stID, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                SearchUserName = searchUserName,
                STID = stID,
                PageIndex = pageIndex
            };

            int _totalNums = 0;

            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;
        }

        public void XHrList(string searchUserName, int stID, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                SearchUserName = searchUserName,
                STID = stID,
                PageIndex = pageIndex
            };

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;

            int _totalNums = 0, _pageSize = 12;
            string _conAttr = "1 = 1";
            if (!string.IsNullOrEmpty(searchUserName))
            {
                _conAttr += " AND Employee.Name = '" + searchUserName + "'";
            }
            if (stID > 0)
            {
                _conAttr += " AND Registration.STID = " + stID;
            }

            IList<Registration> _registrationList = _registrationService.PaginatedRegistration(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            string uri = "javascript:jsPaginated('" + searchUserName + "'," + stID + ",{0});";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["registrationList"] = _registrationList;
            PropertyBag["ltlShowPager"] = ltlShowPager;

            CancelLayout();
        }

        public void PostDelete(string registrationIDList)
        {
            int _code = 0;
            string _message = "删除学员报名记录失败";

            if (!string.IsNullOrEmpty(registrationIDList))
            {
                string[] _registrationIDList = registrationIDList.Split(',');
                for (int i = 0; i < _registrationIDList.Length; i++)
                {
                    int _registrationID = 0;
                    int.TryParse(_registrationIDList[i], out _registrationID);

                    if (_registrationID > 0)
                    {
                        _code = _registrationService.DeleteRegistrationByID(_registrationID);
                        if (_code > 0)
                            _message = "删除学员报名记录成功";
                    }
                }
            }
            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int registrationID, string searchUserName, int stID, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                SearchUserName = searchUserName,
                STID = stID,
                PageIndex = pageIndex
            };

            Employee _logonUser = Session["logonUser"] as Employee;

            int _totalNums = 0, _pageSize = 12;
            string _conAttr = "1 = 1";
            if (_logonUser.UserType == 2 && _logonUser.GradeID > 0)
                _conAttr = "Grade.ID = " + _logonUser.GradeID;

            IList<Grade> _gradeList = _gradeService.PaginatedGrade(1, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["gradeList"] = _gradeList;

            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

            Registration _registration = _registrationService.GetRegistrationByID(registrationID);
            PropertyBag["registration"] = _registration;
        }

        //"name","lgID","schoolFee","remark"
        public void PostEdit(int registrationID, string employeeName, string schoolName, int gradeID, string gradeName, int stID, int schoolFee, string remark)
        {
            int _code = 0;
            string _message = "修改学员报名信息失败";
            if (!string.IsNullOrEmpty(employeeName))
            {
                Employee _employee = _employeeService.GetEmployeeByName(employeeName);
                if (_employee != null)
                {
                    Registration _registration = _registrationService.GetRegistrationByID(registrationID);
                    if (_registration != null)
                    {
                        _registration.ID = registrationID;
                        _registration.EmployeeID = _employee.ID;
                        _registration.SchoolName = schoolName;
                        _registration.GradeID = gradeID;
                        _registration.GradeName = gradeName;

                        _registration.STID = stID;
                        _registration.Status = 0;
                        _registration.SchoolFee = schoolFee;
                        _registration.Remark = remark;
                        _code = _registrationService.UpdateRegistration(_registration);

                        if (_code > 0)
                        {
                            _message = "修改学员报名信息成功";
                        }
                        else
                        {
                            _message = "对不起，修改学员报名信息失败";
                        }
                    }
                    else
                    {
                        _code = -1;
                        _message = "对不起，学员报名信息不存在";
                    }
                }
                else
                {
                    _message = "此学籍号还没有在系统中登记";
                }
            }
            else
            {
                _message = "学籍号不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostEmbedEdit(int id, string field, string value)
        {
            string _result = value;
            Registration _registration = _registrationService.GetRegistrationByID(id);
            if (_registration != null)
            {
                field = field.ToLower();
                switch (field)
                {
                    case "schoolname":
                        _registration.SchoolName = value;
                        break;
                    case "gradename":
                        _registration.GradeName = value;
                        break;
                    case "gradeid":
                        {
                            int _gradeID = 0;
                            if (int.TryParse(value, out _gradeID))
                            {
                                _registration.GradeID = _gradeID;
                                _result = _gradeService.GetGradeByID(_gradeID).Name;
                            }
                            break;
                        }
                    case "stid":
                        {
                            int _stID = 0;
                            if (int.TryParse(value, out _stID))
                            {
                                _registration.STID = _stID;
                                _result = _schoolTermService.GetBaseSchoolTermByID(_stID).Name;
                            }
                            break;
                        }
                    case "schoolfee":
                        {
                            decimal _schoolFee = 0;
                            if (decimal.TryParse(value, out _schoolFee))
                            {
                                _registration.SchoolFee = _schoolFee;
                            }
                            break;
                        }
                    case "status":
                        {
                            int _status = 0;
                            if (int.TryParse(value, out _status))
                            {
                                _registration.Status = _status;
                            }
                            break;
                        }
                    case "refund":
                        {
                            decimal _refund = 0;
                            if (decimal.TryParse(value, out _refund))
                            {
                                _registration.Refund = _refund;
                            }
                            break;
                        }
                    case "reasons":
                        _registration.Reasons = value;
                        break;
                    case "assessor":
                        _registration.Assessor = value;
                        break;
                    case "remark":
                        _registration.Remark = value;
                        break;
                }
                _registrationService.UpdateRegistration(_registration);

                RenderText(_result);
                CancelLayout();
            }
        }

        public void Assessment(int employeeID, string searchUserName, int stID, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                SearchUserName = searchUserName,
                STID = stID,
                PageIndex = pageIndex
            };

            Employee _logonUser = Session["logonUser"] as Employee;

            IList<Registration> _registrationList = null;

            if (employeeID > 0)
            {
                Employee _employee = _employeeService.GetEmployeeByID(employeeID);

                if (_employee != null)
                {
                    Registration _registration = new Registration();
                    _registration.EmployeeID = _employee.ID;

                    if (_logonUser.UserType == 2 && _logonUser.GradeID > 0)
                        _registration.GradeID = _logonUser.GradeID;

                    _registrationList = _registrationService.GetRegistration(_registration);

                    PropertyBag["employee"] = _employee;
                    PropertyBag["registrationList"] = _registrationList;
                }
            }
        }

        public void PostAssessment(int employeeID, int stID, string judgment)
        {
            int _code = 0;
            string _message = "考评录入失败";

            if (!string.IsNullOrEmpty(judgment))
            {
                Employee _employee = _employeeService.GetBaseEmployeeByID(employeeID);

                if (_employee != null)
                {
                    Assessment _assessment = new Assessment();
                    _assessment.EmployeeID = employeeID;
                    _assessment.STID = stID;
                    IList<Assessment> _assessmentList = _assessmentService.GetAssessment(_assessment);
                    if (_assessmentList.Count > 0)
                    {
                        _assessment = _assessmentList[0];
                        _assessment.TrueName = _employee.TrueName;
                        _assessment.Judgment = judgment;

                        _code = _assessmentService.UpdateAssessment(_assessment);
                        _message = "修改考评记录成功！";
                    }
                    else
                    {
                        _assessment.TrueName = _employee.TrueName;
                        _assessment.Judgment = judgment;
                        _code = _assessmentService.InsertAssessment(_assessment);
                        _message = "录入考评记录成功！";
                    }
                }
                else
                {
                    _code = -1;
                    _message = "对不起，用户不存在！";
                }
            }
            else
            {
                _message = "对不起，评语不能为空！";
            }
            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
        public void Refund(int registrationID,string searchUserName, int stID, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                SearchUserName = searchUserName,
                STID = stID,
                PageIndex = pageIndex
            };

            Registration _registration = _registrationService.GetRegistrationByID(registrationID);
            if (_registration != null) {

                Employee _employee = _employeeService.GetEmployeeByID(_registration.EmployeeID.Value);
                PropertyBag["employee"] = _employee;
            }

            PropertyBag["registration"] = _registration;
        }

        //"classMembersID","schoolFee","status","refund","reasons","remark"
        public void PostEditRefund(int registrationID, int schoolFee, int status, int refund, string reasons, string remark)
        {
            Employee _logonUser = Session["logonUser"] as Employee;
            int _code = 0;
            string _message = "办理退课失败";

            Registration _registration = _registrationService.GetRegistrationByID(registrationID);
            if (_registration != null)
            {
                _registration.Status = status;
                _registration.SchoolFee = schoolFee;

                if (status == -1)
                {
                    _registration.Refund = refund;
                    _registration.Reasons = reasons;
                    _registration.Assessor = _logonUser == null ? "" : _logonUser.TrueName;
                }
                else
                {
                    _registration.Refund = 0;
                    _registration.Reasons = string.Empty;
                    _registration.Assessor = _logonUser == null ? "" : _logonUser.TrueName;
                }
                _code = _registrationService.UpdateRegistration(_registration);
                if (_code > 0)
                {
                    _message = "设置成功";
                }
            }
            RenderText("{\"code\":" + _code + ",\"message\":\"" + _message + "\"}");
            CancelLayout();
        }

    }
}
