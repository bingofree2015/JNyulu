using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MonoRail.Framework;

using Newtonsoft.Json;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using System.IO;
using JNyuluSoft.Util;
using System.Text;

namespace JNyuluSoft.Web.Controllers
{
    public class HelperController : DefaultController
    {
        private string _uploadFilePath = JNyuluUtils.UploadFilePath;

        protected readonly CatalogService _catalogService = CatalogService.GetInstance();

        protected readonly CampusService _campusService = CampusService.GetInstance();
        protected readonly SchoolRoomService _schoolRoomService = SchoolRoomService.GetInstance();

        protected readonly GradeService _gradeService = GradeService.GetInstance();

        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();
        protected readonly RegistrationService _registrationService = RegistrationService.GetInstance();

        public void CatalogList(string moduleCn = "Video", int catalogID = 0)
        {
            int _totalNums = 0, _pageSize = 120, pageIndex = 1;
            string _conAttr = "moduleCn = '" + moduleCn + "' AND fatherID = " + catalogID;

            IList<Catalog> _catalogList = _catalogService.PaginatedCatalog(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["catalogList"] = _catalogList;

            CancelLayout();
        }

        public void GradeList()
        {
            Employee _logonUser = Session["logonUser"] as Employee;

            int _totalNums = 0, _pageSize = 120, pageIndex = 1;
            string _conAttr = "1 = 1";
            if (_logonUser.UserType == 2 && _logonUser.GradeID > 0)
                _conAttr = "Grade.ID = " + _logonUser.GradeID;

            IList<Grade> _gradeList = _gradeService.PaginatedGrade(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["gradeList"] = _gradeList;

            CancelLayout();
        }

        public void CourseList(int? gradeID, int? schoolTermID)
        {
            Employee _logonUser = Session["logonUser"] as Employee;

            string _conAttr = "1 = 1";
            if (_logonUser.GradeID > 0 && (_logonUser.UserType == 2))
            {
                gradeID = _logonUser.GradeID;
            }

            if (gradeID > 0)
                _conAttr = "gradeID = " + gradeID;
            else if (schoolTermID > 0)
            {
                _conAttr = "GradeID IN(SELECT [GradeID] FROM [SchoolTerm] WHERE ID=" + schoolTermID + ")";
            }

            CancelLayout();
        }

        public void CampusList()
        {
            int _totalNums = 0, _pageSize = 120, pageIndex = 1;
            string _conAttr = "1 = 1";

            IList<Campus> _campusList = _campusService.PaginatedCampus(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["campusList"] = _campusList;

            CancelLayout();
        }

        public void SchoolRoomList(int campusID)
        {
            int _totalNums = 0, _pageSize = 120, pageIndex = 1;
            string _conAttr = "campusID = " + campusID;

            IList<SchoolRoom> _schoolRoomList = _schoolRoomService.PaginatedSchoolRoom(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["schoolRoomList"] = _schoolRoomList;

            CancelLayout();
        }

        public void SchoolTermList(int? gradeID)
        {
            Employee _logonUser = Session["logonUser"] as Employee;

            int _totalNums = 0, _pageSize = 120, pageIndex = 1;
            string _conAttr = "1 = 1";

            if (_logonUser.GradeID > 0 && (_logonUser.UserType == 2))
            {
                gradeID = _logonUser.GradeID;
            }

            if (gradeID > 0)
                _conAttr = "gradeID = " + gradeID;

            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

            CancelLayout();
        }

        public void EmployeeCheckList(int lgID)
        {
            int _totalNums = 0, _pageSize = 120, pageIndex = 1;
            string _conAttr = "1 = 1";

            if (lgID > 0)
                _conAttr = "LGID = " + lgID;

            IList<Registration> _registrationList = _registrationService.PaginatedRegistration(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["RegistrationList"] = _registrationList;

            CancelLayout();
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void UploadFiles(string editor, int returnType = 0)
        {
            int _code = 0;
            string _message = string.Empty;
            String _fileExt = string.Empty;

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
                    String[] _allowedExt = { ".gif", ".jpg", ".png", ".zip", ".rar", ".doc", ".swf", ".xls" }; //允许上传的文件格式

                    if (!_allowedExt.Contains(_fileExt))
                    {
                        _message = "系统只接受扩展名为" + string.Join("、", _allowedExt) + "格式文件";
                        _code = -3;
                    }
                    else if (_uploadFiles.ContentLength == 0)
                    {
                        _message = "上传的内容为空！";
                        _code = -2;
                    }
                    else if (_uploadFiles.ContentLength > 1024 * 1024 * 10)
                    {
                        _message = "您上传的文件超过了10M！";
                        _code = -1;
                    }
                }

                if (_code == 0)
                {
                    string _fileName = ShortFileNameUtil.ShortFileName(Guid.NewGuid().ToString()) + _fileExt;
                    _uploadFilePath += _fileExt.TrimStart('.') + "/" + DateTime.Today.ToString("yyyy-MM-dd") + @"/";
                    if (!Directory.Exists(_uploadFilePath))
                    {
                        Directory.CreateDirectory(_uploadFilePath);
                    }

                    string _savePath = _uploadFilePath + _fileName;
                    _uploadFiles.SaveAs(_savePath);

                    _message = "/UploadFiles/" + _fileExt.TrimStart('.') + "/" + DateTime.Today.ToString("yyyy-MM-dd") + @"/" + _fileName;
                }
            }
            catch (Exception ex)
            {
                _code = -1;
                _message = ex.Message;
            }
            finally
            {
                if (returnType == 1 && _code == 0)
                {
                    StringBuilder _jsString = new StringBuilder();

                    _jsString.AppendLine("<script>");
                    _jsString.AppendLine("window.parent." + editor + ".insertContent(\"<img src='" + _message + "'/>\")");
                    _jsString.AppendLine("window.parent." + editor + ".plugins.upload.finish();");
                    _jsString.AppendLine("</script>");

                    RenderText(_jsString.ToString());
                }
                else
                {
                    RenderText("{\"code\":\"" + _code + "\",\"message\": \"" + _message.Replace(@"\", @"\\") + "\"}");
                }

                CancelLayout();
            }
        }
    }
}