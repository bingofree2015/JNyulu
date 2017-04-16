using System;
using System.IO;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;

using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using JNyuluSoft.Util;

namespace JNyuluSoft.Web.Controllers
{
    /* 学生课程管理 */
    [ControllerDetails(Area = "Background")]
    public class ScheNoticeController : DefaultController
    {
        protected readonly AssessmentService _assessmentService = AssessmentService.GetInstance();
        protected readonly ScheNoticeService _scheNoticeService = ScheNoticeService.GetInstance();
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();
        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();
        protected readonly RegistrationService _registrationService = RegistrationService.GetInstance();

        public void ImportExcel(int pageIndex)
        {
            PropertyBag["params"] = new
            {
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
                    string _virtualPath = "/UploadFiles/scheNoticeXls/";

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
                        try
                        {
                            //dr["电话"] dr["姓名"] dr["内容"]
                            string _userName = dr["电话"].ToString() + "";
                            string _trueName = dr["姓名"].ToString() + "";
                            string _context = dr["内容"].ToString() + "";

                            if (!string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(_trueName) && !string.IsNullOrEmpty(_context))
                            {
                                ScheNotice _scheNotice = new ScheNotice();
                                _scheNotice.UserName = _userName;
                                _scheNotice.TrueName = _trueName;
                                _scheNotice.Context = _context;

                                {
                                    _code = _scheNoticeService.InsertScheNotice(_scheNotice);
                                }
                                if (_code > 0)
                                {
                                    LogHelper.Info("记录[" + _i + "]导入成功");
                                    _result.AppendLine("记录[" + _i + "]导入成功");
                                }

                                _code = _scheNotice.ID;
                            }
                        }
                        catch (Exception ex)
                        {
                            _result.AppendLine(ex.ToString());
                            LogHelper.Error(ex.ToString());
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

        public void List(string searchName, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;

            PropertyBag["params"] = new
            {
                PageIndex = pageIndex,
                SearchName = searchName
            };

            int _totalNums = 0, _pageSize = 10;
            string _conAttr = "1 = 1";

            if (!string.IsNullOrEmpty(searchName))
            {
                _conAttr += " AND trueName LIKE '%" + searchName + "%'";
            }

            IList<ScheNotice> _scheNoticeList = _scheNoticeService.PaginatedScheNotice(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["scheNoticeList"] = _scheNoticeList;

            string uri = "List.do?searchName=" + searchName + "&pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void PostBatchDelete(string scheNoticeIDs)
        {
            int _code = 0;
            string _message = "删除开课通知时失败";

            if (!string.IsNullOrEmpty(scheNoticeIDs))
            {
                string[] _scheNoticeIDs = scheNoticeIDs.Split(',');

                int _scheNoticeID = 0;

                for (int i = 0; i < _scheNoticeIDs.Length; i++)
                {
                    int.TryParse(_scheNoticeIDs[i], out _scheNoticeID);
                    if (_scheNoticeID > 0)
                    {
                        _code = _scheNoticeService.DeleteScheNoticeByID(_scheNoticeID);

                        if (_code > 0)
                            _message = "删除开课通知成功";
                    }
                }
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostSave(string ids, string userNames, string trueNames)
        {
            int _code = 0;
            string _message = "开课通知处理失败";
            string[] _idList = ids.Split('|');
            string[] _userNameList = userNames.Split('|');
            string[] _trueNameList = trueNames.Split('|');

            if ((_idList.Length == _userNameList.Length) && _idList.Length > 0)
            {
                int _id = 0;
                string _userName = string.Empty;
                string _trueName = string.Empty;
                StringBuilder _result = new StringBuilder("开课通知处理结果：");

                for (int i = 0; i < _idList.Length; i++)
                {
                    if (int.TryParse(_idList[i], out _id))
                    {
                        _userName = _userNameList[i];
                        _trueName = _trueNameList[i];

                        ScheNotice _scheNotice = _scheNoticeService.GetScheNoticeByID(_id);
                        _scheNotice.UserName = _userName;
                        _scheNotice.TrueName = _trueName;

                        _code = _scheNoticeService.UpdateScheNotice(_scheNotice);
                        _result.AppendLine("编号" + _scheNotice.ID + "修改成功,编号:" + _code);
                    }
                }
                _message = "修改成功";
            }
            else
            {
                _message = "对不起，可能数据格式不对！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void UploadScheNotice(int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };
        }
    }
}
