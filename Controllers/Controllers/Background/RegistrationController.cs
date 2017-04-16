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
    /* ������Ϣ¼�� */
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
            string _message = "�����༶��Աʧ��";
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
                            _message = "�����༶��Ա�ɹ�";
                        }
                        else
                        {
                            _message = "��ͬѧ�Ѿ�ע��";
                        }
                    }
                }
                else
                {
                    _message = "ѧ���Ų�����";
                }
            }
            else
            {
                _message = "ѧ���Ų���Ϊ�գ�";
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
                    _result.AppendLine("�ϴ�·��Ϊ��" + _virtualPath + _fileName);
                    _result.AppendLine("Excel������־��¼��");
                    foreach (DataRow dr in _employeeData.Rows)
                    {
                        _i++;
                        try
                        {
                            //["ѧ����"] ["ѧ������"] dr["����ʱ��"] dr["�༶"] dr["ԭУ"] dr["ԭ���꼶"] dr["���"] dr["��ע"]
                            string _name = dr["ѧ����"].ToString() + "";
                            if (!string.IsNullOrEmpty(_name))
                            {
                                Employee _employee = _employeeService.GetEmployeeByName(_name);
                                if (_employee != null)
                                {
                                    string _gradeName = dr["ԭ���꼶"].ToString() + "";
                                    Grade _grade = _gradeService.GetGradeByName(_gradeName);
                                    if (_grade != null)
                                    {
                                        int _stID = 0;
                                        string _schoolTermName = dr["�༶"].ToString();
                                        SchoolTerm _schoolTerm = _schoolTermService.GetBaseSchoolTermByName(_schoolTermName);
                                        if (_schoolTerm != null)
                                        {
                                            _stID = _schoolTerm.ID;
                                        }

                                        int _schoolFee = 0;
                                        int.TryParse(dr["���"].ToString(), out _schoolFee);

                                        Registration _registration = new Registration();
                                        _registration.EmployeeID = _employee.ID;

                                        _registration.SchoolName = dr["ԭУ"].ToString() + "";
                                        _registration.GradeID = _grade.ID;
                                        _registration.GradeName = _grade.Name;

                                        _registration.STID = _stID;
                                        _registration.SchoolFee = _schoolFee;
                                        _registration.Remark = dr["��ע"].ToString() + "";

                                        DateTime _recordDate = DateTime.Today;
                                        if (DateTime.TryParse(dr["����ʱ��"].ToString() + "", out _recordDate))
                                        {
                                            _registration.RecordDate = _recordDate;
                                        }
                                        else
                                        {
                                            LogHelper.Info("��¼[" + _i + "]����ʱ���ʽ����");
                                            _result.AppendLine("��¼[" + _i + "]����ʱ���ʽ����");
                                            continue;
                                        }

                                        _code = _registrationService.InsertRegistration(_registration);
                                    }
                                    else
                                    {
                                        LogHelper.Error("��¼[" + _i + "]ԭ���꼶" + _gradeName + "������");
                                        _result.AppendLine("��¼[" + _i + "]ԭ���꼶" + _gradeName + "������");
                                    }
                                }
                                else
                                {
                                    LogHelper.Error("��¼[" + _i + "]ѧ����" + _name + "�����ڣ�������ѡע��ѧ����");
                                    _result.AppendLine("��¼[" + _i + "]ѧ����" + _name + "�����ڣ�������ѡע��ѧ����");
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
            string _message = "ΪѧԱ��д������ʧ��";
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
                        _message = "ΪѧԱ��д������ɹ�";
                    }
                    else
                    {
                        _message = "��ѧԱ�Ѿ�����";
                    }
                }
                else
                {
                    _message = "��ѧ���Ż�û����ϵͳ�еǼ�";
                }
            }
            else
            {
                _message = "ѧ���Ų���Ϊ�գ�";
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
            string _message = "ɾ��ѧԱ������¼ʧ��";

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
                            _message = "ɾ��ѧԱ������¼�ɹ�";
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
            string _message = "�޸�ѧԱ������Ϣʧ��";
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
                            _message = "�޸�ѧԱ������Ϣ�ɹ�";
                        }
                        else
                        {
                            _message = "�Բ����޸�ѧԱ������Ϣʧ��";
                        }
                    }
                    else
                    {
                        _code = -1;
                        _message = "�Բ���ѧԱ������Ϣ������";
                    }
                }
                else
                {
                    _message = "��ѧ���Ż�û����ϵͳ�еǼ�";
                }
            }
            else
            {
                _message = "ѧ���Ų���Ϊ�գ�";
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
            string _message = "����¼��ʧ��";

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
                        _message = "�޸Ŀ�����¼�ɹ���";
                    }
                    else
                    {
                        _assessment.TrueName = _employee.TrueName;
                        _assessment.Judgment = judgment;
                        _code = _assessmentService.InsertAssessment(_assessment);
                        _message = "¼�뿼����¼�ɹ���";
                    }
                }
                else
                {
                    _code = -1;
                    _message = "�Բ����û������ڣ�";
                }
            }
            else
            {
                _message = "�Բ������ﲻ��Ϊ�գ�";
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
            string _message = "�����˿�ʧ��";

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
                    _message = "���óɹ�";
                }
            }
            RenderText("{\"code\":" + _code + ",\"message\":\"" + _message + "\"}");
            CancelLayout();
        }

    }
}
