using System;
using System.IO;
using System.Web;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

using Castle.MonoRail.Framework;

using MyMessage = JNyuluSoft.Model.Message;
using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web.Controllers
{
    /* 学生登录界面  */
    [ControllerDetails(Area = "Background")]
    public class StudentMgtController : DefaultController
    {
        protected readonly RegistrationService _registrationService = RegistrationService.GetInstance();
        protected readonly ReportCardService _reportCardService = ReportCardService.GetInstance();
        protected readonly SyllabusService _syllabusService = SyllabusService.GetInstance();
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();

        protected readonly ArticleService _articleService = ArticleService.GetInstance();

        protected readonly MessageService _messageService = MessageService.GetInstance();
        protected readonly NotificationService _notificationService = NotificationService.GetInstance();

        protected readonly BulletinBoardService _bulletinBoardService = BulletinBoardService.GetInstance();
        protected readonly VideoService _videoService = VideoService.GetInstance();

        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();

        public void Index()
        {
            IList<Registration> _registrationList = null;

            Employee _employee = Session["logonUser"] as Employee;
            if (_employee != null)
            {
                Registration _registration = new Registration();
                _registration.EmployeeID = _employee.ID;

                _registrationList = _registrationService.GetRegistration(_registration);
                foreach (Registration Registration in _registrationList)
                {
                    IDictionary _hash = new Hashtable();

                    _hash.Add("pageIndex", 1);
                    _hash.Add("pageSize", 100);
                    _hash.Add("JOIN", "INNER");
                    _hash.Add("EmployeeID", Registration.EmployeeID);

                    Registration.SyllabusList = _syllabusService.GetSyllabus(_hash);
                }

                PropertyBag["employee"] = _employee;
                PropertyBag["RegistrationList"] = _registrationList;
            }
        }

        public void Assessment()
        {
            Employee _employee = Session["logonUser"] as Employee;
            if (_employee != null)
            {
                Registration _registration = new Registration();
                _registration.EmployeeID = _employee.ID;
                IList<Registration> _registrationList = _registrationService.GetRegistration(_registration);

                PropertyBag["RegistrationList"] = _registrationList;
            }
        }

        public void ReportCard()
        {
            IList<Registration> _registrationList = null;

            Employee _employee = Session["logonUser"] as Employee;
            if (_employee != null)
            {
                Registration _registration = new Registration();
                _registration.EmployeeID = _employee.ID;

                _registrationList = _registrationService.GetRegistration(_registration);
                foreach (Registration Registration in _registrationList)
                {
                    IDictionary _hash = new Hashtable();

                    _hash.Add("pageIndex", 1);
                    _hash.Add("pageSize", 100);
                    _hash.Add("JOIN", "INNER");
                    _hash.Add("EmployeeID", Registration.EmployeeID);

                    Registration.ReportCardList = _reportCardService.GetReportCard(_hash);
                }

                PropertyBag["employee"] = _employee;
                PropertyBag["registrationList"] = _registrationList;
            }
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

        public void BulletinBoard(int pageIndex)
        {
            Employee _employee = Session["logonUser"] as Employee;
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            int _pageSize = 12;

            string _conAttr = "(Receiver = 0) OR Receiver IN(SELECT stID FROM Registration WHERE EmployeeID=" + _employee.ID + ")";

            IList<BulletinBoard> _bulletinBoardList = _bulletinBoardService.PaginatedBulletinBoard(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            string uri = "BulletinBoard.do?pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["bulletinBoardList"] = _bulletinBoardList;
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void BulletinBoardDetail(int pageIndex, int bulletinBoardID)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            BulletinBoard _bulletinBoard = _bulletinBoardService.GetBulletinBoardByID(bulletinBoardID);
            PropertyBag["bulletinBoard"] = _bulletinBoard;
        }

        public void Notification(int pageIndex)
        {
            Employee _employee = Session["logonUser"] as Employee;

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            int _pageSize = 12;

            string _conAttr = "Receivers LIKE '%," + _employee.Name + ",%' OR stID IN(SELECT stID FROM [JNyulu_db].[dbo].[Registration] WHERE EmployeeID = " + _employee.ID + ") OR stID = 0";

            IList<Notification> _notificationList = _notificationService.PaginatedNotification(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            string uri = "Notification.do?pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["notificationList"] = _notificationList;
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }


        public void Message(int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            Employee _employee = Session["logonUser"] as Employee;

            int _totalNums = 0;
            int _pageSize = 12;

            string _conAttr = "userName = '" + _employee.Name + "'";

            IList<Message> _messageList = _messageService.PaginatedMessage(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            string uri = "Message.do?pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["messageList"] = _messageList;
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void CreateMessage(int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };
        }

        public void PostCreateMessage(string subject, string issue)
        {
            Employee _employee = Session["logonUser"] as Employee;

            int _code = 0;
            string _message = "操作失败";

            if (!string.IsNullOrEmpty(subject))
            {
                MyMessage _myMessage = new MyMessage();
                _myMessage.UserName = _employee.Name;
                _myMessage.Subject = subject;
                _myMessage.Issue = issue;

                _code = _messageService.InsertMessage(_myMessage);
                _message = "您的问题已经成功提交，请等候回复...";
            }
            else
            {
                _message = "对不起，标题不能为空！";
            }
            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Article(int catalogID, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                CatalogID = catalogID,
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            int _pageSize = 12;

            string _conAttr = "CatalogID =" + catalogID;

            IList<Article> _articleList = _articleService.PaginatedArticle(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            string uri = "Article.do?catalogID=" + catalogID + "&pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["articleList"] = _articleList;
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void ArticleDetail(int catalogID, int pageIndex, int articleID)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                CatalogID = catalogID,
                PageIndex = pageIndex
            };

            Article _article = _articleService.GetArticleByID(articleID);
            PropertyBag["article"] = _article;
        }

        public void Video(int pageIndex, int stID = 0)
        {
            Employee _employee = Session["logonUser"] as Employee;

            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                STID = stID,
                PageIndex = pageIndex
            };

            int _totalNums = 0, _pageSize = 8;

            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "SchoolTerm.ID", "SchoolTerm.ID IN(SELECT [STID] FROM [JNyulu_db].[dbo].[Registration](NOLOCK) WHERE [EmployeeID] = " + _employee.ID + ")", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;
            if (_schoolTermList != null && _schoolTermList.Count > 0 && stID == 0) {
                stID = _schoolTermList[0].ID;
            }

            string _conAttr = "STID IN (SELECT STID FROM [Registration] WHERE EmployeeID = " + _employee.ID + ")";
            if (stID > 0)
            {
                _conAttr += " AND STID = " + stID;
            }

            IList<Video> _videoList = _videoService.PaginatedVideo(pageIndex, _pageSize, "SortNum", _conAttr, ref _totalNums);
            PropertyBag["videoList"] = _videoList;

            string uri = "Video.do?stID=" + stID + "&pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void VideoDetail(int pageIndex, int stID, int videoID)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                STID = stID,
                PageIndex = pageIndex
            };

            Video _video = _videoService.GetVideoByID(videoID);
            PropertyBag["video"] = _video;
        }
    }
}
