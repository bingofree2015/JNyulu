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
    /* ѧ���γ̹��� */
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
                    _result.AppendLine("�ϴ�·��Ϊ��" + _virtualPath + _fileName);
                    _result.AppendLine("Excel������־��¼��");
                    foreach (DataRow dr in _employeeData.Rows)
                    {
                        try
                        {
                            //dr["�绰"] dr["����"] dr["����"]
                            string _userName = dr["�绰"].ToString() + "";
                            string _trueName = dr["����"].ToString() + "";
                            string _context = dr["����"].ToString() + "";

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
                                    LogHelper.Info("��¼[" + _i + "]����ɹ�");
                                    _result.AppendLine("��¼[" + _i + "]����ɹ�");
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
            string _message = "ɾ������֪ͨʱʧ��";

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
                            _message = "ɾ������֪ͨ�ɹ�";
                    }
                }
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostSave(string ids, string userNames, string trueNames)
        {
            int _code = 0;
            string _message = "����֪ͨ����ʧ��";
            string[] _idList = ids.Split('|');
            string[] _userNameList = userNames.Split('|');
            string[] _trueNameList = trueNames.Split('|');

            if ((_idList.Length == _userNameList.Length) && _idList.Length > 0)
            {
                int _id = 0;
                string _userName = string.Empty;
                string _trueName = string.Empty;
                StringBuilder _result = new StringBuilder("����֪ͨ��������");

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
                        _result.AppendLine("���" + _scheNotice.ID + "�޸ĳɹ�,���:" + _code);
                    }
                }
                _message = "�޸ĳɹ�";
            }
            else
            {
                _message = "�Բ��𣬿������ݸ�ʽ���ԣ�";
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
