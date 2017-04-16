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
    public class SyllabusController : DefaultController
    {
        protected readonly AssessmentService _assessmentService = AssessmentService.GetInstance();
        protected readonly SyllabusService _syllabusService = SyllabusService.GetInstance();
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();
        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();
        protected readonly RegistrationService _registrationService = RegistrationService.GetInstance();

        public void ImportExcel(int stID, int pageIndex)
        {
            PropertyBag["params"] = new
            {
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
                    string _virtualPath = "/UploadFiles/syllabusXls/";

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
                            //dr["班级"] dr["学籍号"] dr["学生姓名"] dr["课程表路径"];
                            string _name = dr["学籍号"].ToString() + "";
                            Employee _employee = _employeeService.GetEmployeeByName(_name);

                            if (_employee == null)
                            {
                                LogHelper.Info("记录[" + _i + "]学籍:" + _name + "  不存在");
                                _result.AppendLine("记录[" + _i + "]学籍:" + _name + "  不存在");
                                continue;
                            }

                            int _stID = 0;
                            string _schoolTermName = dr["班级"].ToString() + "";
                            SchoolTerm _schoolTerm = _schoolTermService.GetBaseSchoolTermByName(_schoolTermName);

                            if (_schoolTerm == null)
                            {
                                LogHelper.Info("记录[" + _i + "]班级:" + _schoolTermName + "  不存在");
                                _result.AppendLine("记录[" + _i + "]班级:" + _schoolTermName + "  不存在");
                                continue;
                            }

                            if (_employee.ID > 0 && _stID > 0)
                            {
                                Syllabus _syllabus = new Syllabus();
                                _syllabus.EmployeeID = _employee.ID;
                                _syllabus.STID = _stID;

                                _syllabus.SyllabusUrl = _virtualPath + dr["课程表路径"].ToString() + "";

                                IList<Syllabus> _syllabusList = _syllabusService.GetBaseSyllabus(_syllabus);
                                if (_syllabusList.Count > 0)
                                {
                                    _code = _syllabusList[0].ID;
                                    _syllabusService.UpdateSyllabus(_syllabus);
                                }
                                else
                                {
                                    _code = _syllabusService.InsertSyllabus(_syllabus);
                                }
                                if (_code > 0)
                                {
                                    LogHelper.Info("记录[" + _i + "]导入成功");
                                    _result.AppendLine("记录[" + _i + "]导入成功");
                                }

                                _code = _syllabus.ID;
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

        public void List(int stID, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                STID = stID < 0 ? 0 : stID,
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;
        }

        public void XHrList(int stID, int pageIndex)
        {
            int _totalNums = 0, _pageSize = 12;
            string _conAttr = "1 = 1";

            if (stID > 0)
            {
                _conAttr += " AND STID =" + stID;
            }

            IList<Syllabus> _syllabusList = _syllabusService.PaginatedSyllabus(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["syllabusList"] = _syllabusList;

            string uri = "javascript:jsPaginated(" + stID + ",{0});";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);
            PropertyBag["ltlShowPager"] = ltlShowPager;

            CancelLayout();
        }

        public void PostBatchDelete(string syllabusIDs)
        {
            int _code = 0;
            string _message = "删除学员成绩表时失败";

            if (!string.IsNullOrEmpty(syllabusIDs))
            {
                string[] _syllabusIDs = syllabusIDs.Split(',');

                int _syllabusID = 0;

                for (int i = 0; i < _syllabusIDs.Length; i++)
                {
                    int.TryParse(_syllabusIDs[i], out _syllabusID);
                    if (_syllabusID > 0)
                    {
                        _code = _syllabusService.DeleteSyllabusByID(_syllabusID);

                        if (_code > 0)
                            _message = "删除学员成绩表成功";
                    }
                }
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostCreate(string employeeIDs, string stIDs, string syllabusUrls)
        {
            int _code = 0;
            string _message = "成绩处理失败";
            string[] _employeeIDList = employeeIDs.Split('|');
            string[] _stIDList = stIDs.Split('|');
            string[] _syllabusUrlList = syllabusUrls.Split('|');

            if ((_employeeIDList.Length == _syllabusUrlList.Length) && _syllabusUrlList.Length > 0)
            {
                int _employeeID = 0;
                int _stID = 0;
                string _syllabusUrl = string.Empty;
                StringBuilder _result = new StringBuilder("成绩处理结果：");

                for (int i = 0; i < _employeeIDList.Length; i++)
                {
                    if (int.TryParse(_employeeIDList[i], out _employeeID)
                        && int.TryParse(_stIDList[i], out _stID)
                        )
                    {
                        _syllabusUrl = _syllabusUrlList[i];

                        Syllabus _syllabus = new Syllabus();
                        _syllabus.EmployeeID = _employeeID;
                        _syllabus.STID = _stID;
                        IList<Syllabus> _syllabusList = _syllabusService.GetBaseSyllabus(_syllabus);
                        if (_syllabusList.Count == 0)
                        {
                            _syllabus = new Syllabus();
                            _syllabus.EmployeeID = _employeeID;
                            _syllabus.STID = _stID;
                            _syllabus.SyllabusUrl = _syllabusUrl;

                            _code = _syllabusService.InsertSyllabus(_syllabus);
                            _result.AppendLine("学籍号" + _employeeID + "录入成功,编号:" + _code);
                        }
                        else
                        {
                            _syllabus = _syllabusList[0];

                            _syllabus.SyllabusUrl = _syllabusUrl;

                            _code = _syllabusService.UpdateSyllabus(_syllabus);

                            _result.AppendLine("学籍号" + _employeeID + "修改成功,编号:" + _code);
                        }
                    }
                }
                _message = _result.ToString();
            }
            else
            {
                _message = "对不起，可能数据格式不对！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void UploadSyllabus(int stID, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                STID = stID,
                PageIndex = pageIndex
            };
        }

        public void PostUploadSyllabus()
        {
            string _uploadFilePath = JNyuluUtils.UploadFilePath + @"syllabusFiles\";

            string _name = string.Empty;
            int _chunk = 0;
            int _chunks = 0;

            int _code = 0;
            string _message = string.Empty;

            if (HttpContext.Request["chunk"] != null)
            {
                _chunk = int.Parse(HttpContext.Request["chunk"]);//当前分块
            }
            if (HttpContext.Request["chunks"] != null)
            {
                _chunks = int.Parse(HttpContext.Request["chunks"]);//总的分块数量
            }
            _name = HttpContext.Request["name"];

            HttpContext.Response.CacheControl = "no-cache";
            HttpContext.Response.ContentType = "text/plain";

            try
            {
                for (int i = 0; i < HttpContext.Request.Files.Count; i++)
                {
                    if (!Directory.Exists(_uploadFilePath))
                    {
                        Directory.CreateDirectory(_uploadFilePath);
                    }

                    HttpPostedFile _postedFile = HttpContext.Request.Files[i];
                    string _postedFileName = string.Empty;
                    //文件没有分块
                    if (_chunks == 1)
                    {
                        if (_postedFile.ContentLength > 0)
                        {
                            _postedFileName = _postedFile.FileName;
                            _postedFile.SaveAs(string.Format("{0}{1}", _uploadFilePath, _postedFileName));

                            _message = _postedFileName;
                        }
                    }
                    else
                    {
                        //文件 分成多块上传
                        string _tempFileName = WriteTempFile(HttpContext.Session.SessionID, _postedFile, _chunk);
                        if (_chunks - _chunk == 1)
                        {
                            //如果是最后一个分块文件 ，则把文件从临时文件夹中移到上传文件夹中
                            FileInfo _fileInfo = new FileInfo(_tempFileName);

                            _postedFileName = _postedFile.FileName;

                            string _saveFileName = string.Format("{0}{1}", _uploadFilePath, _postedFileName);
                            FileInfo _saveFileInfo = new FileInfo(_saveFileName);
                            if (_saveFileInfo.Exists)
                            {
                                //文件名存在则删除旧文件 
                                _saveFileInfo.Delete();
                            }
                            _fileInfo.MoveTo(_saveFileName);

                            _message = _postedFileName;
                        }
                    }
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

        /// <summary>
        /// 保存临时文件 
        /// </summary>
        /// <param name="uploadFile"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        private string WriteTempFile(string sessionID, HttpPostedFile uploadFile, int chunk)
        {
            string _tempUploadFilePath = JNyuluUtils.TempUploadFilePath;
            if (!Directory.Exists(_tempUploadFilePath))
            {
                Directory.CreateDirectory(_tempUploadFilePath);
            }

            string _tempFullName = string.Format("{0}{1}{2}.part", _tempUploadFilePath, sessionID, uploadFile.FileName);
            if (chunk == 0)
            {
                //如果是第一个分块，则直接保存
                uploadFile.SaveAs(_tempFullName);
            }
            else
            {
                //如果是其他分块文件 ，则原来的分块文件，读取流，然后文件最后写入相应的字节
                FileStream _fileStream = new FileStream(_tempFullName, FileMode.Append);
                if (uploadFile.ContentLength > 0)
                {
                    int _fileLength = uploadFile.ContentLength;
                    byte[] _inputByte = new byte[_fileLength];

                    // Initialize the stream.
                    Stream _myStream = uploadFile.InputStream;
                    // Read the file into the byte array.
                    _myStream.Read(_inputByte, 0, _fileLength);

                    _fileStream.Write(_inputByte, 0, _fileLength);
                    _fileStream.Close();
                }
            }

            return _tempFullName;
        }
    }
}
