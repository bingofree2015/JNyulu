using System;
using System.IO;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

using MyMessage = JNyuluSoft.Model.Message;

using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using JNyuluSoft.Util;

namespace JNyuluSoft.Web.Controllers
{
    public class WebController : DefaultController
    {
        protected readonly EmployeeService _employeeService = EmployeeService.GetInstance();
        protected readonly CatalogService _catalogService = CatalogService.GetInstance();
        protected readonly ArticleService _articleService = ArticleService.GetInstance();

        protected readonly BulletinBoardService _bulletinBoardService = BulletinBoardService.GetInstance();

        protected readonly MessageService _messageService = MessageService.GetInstance();
        protected readonly RegistrationService _registrationService = RegistrationService.GetInstance();

        protected readonly SyllabusService _syllabusService = SyllabusService.GetInstance();
        protected readonly ScheNoticeService _scheNoticeService = ScheNoticeService.GetInstance();

        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();

        protected readonly PhotoGraphService _photoGraphService = PhotoGraphService.GetInstance();
        protected readonly VideoService _videoService = VideoService.GetInstance();

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void Index()
        {
            PropertyBag["catalog"] = 0;
            int _totalNums = 0, _pageSize = 4, pageIndex = 1;

            IList<PhotoGraph> _photoGrapList1 = _photoGraphService.PaginatedPhotoGraph(pageIndex, _pageSize, "SortNum", "catalogID = 1", ref _totalNums);
            PropertyBag["photoGrapList1"] = _photoGrapList1;
            _pageSize = 6;
            IList<PhotoGraph> _photoGrapList2 = _photoGraphService.PaginatedPhotoGraph(pageIndex, _pageSize, "SortNum", "catalogID = 2", ref _totalNums);
            PropertyBag["photoGrapList2"] = _photoGrapList2;

            IList<PhotoGraph> _photoGrapList4 = _photoGraphService.PaginatedPhotoGraph(pageIndex, 1, "SortNum", "catalogID = 4", ref _totalNums);
            if (_photoGrapList4.Count > 0)
            {
                PropertyBag["photoGrap"] = _photoGrapList4[0];
            }

            _pageSize = 8;
            string _conAttr = "catalogID = 2";
            IList<Article> _artilceList2 = _articleService.PaginatedArticle(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["artilceList2"] = _artilceList2;

            _conAttr = "catalogID = 3";
            IList<Article> _artilceList3 = _articleService.PaginatedArticle(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["artilceList3"] = _artilceList3;

            _conAttr = "catalogID = 4";
            IList<Article> _artilceList4 = _articleService.PaginatedArticle(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["artilceList4"] = _artilceList4;

            IList<Video> _videoList = _videoService.PaginatedVideo(pageIndex, 4, "SortNum", "permit = 0", ref _totalNums);
            PropertyBag["videoList"] = _videoList;

            PhotoGraph  _photoGraph  = _photoGraphService.GetBasePhotoGraphByID(171);
            PropertyBag["photoGraph"] = _photoGraph;
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void Corp()
        {
            PropertyBag["catalog"] = -2;
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void ScheNotice() 
        { 

        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void CheckUserName(string userName)
        {

            int _code = 0;
            string _message = "对不起,没找到您的开课信息";

            IDictionary _hash = new Hashtable();
            _hash.Add("UserName", userName);

            IList<ScheNotice> _scheNoticeList = _scheNoticeService.GetScheNotice(_hash);
            if (_scheNoticeList != null && _scheNoticeList.Count > 0)
            {
                _code = 1;
                _message = "查询成功";
            }
            else
            {
                _code = -1;
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelView();
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void List(int catalogID, int pageIndex)
        {
            Catalog _catalog = _catalogService.GetCatalogByID(catalogID);
            if (_catalog != null)
            {
                int _totalNums = 0;
                int _pageSize = 20;
                if (pageIndex < 1) pageIndex = 1;

                string _conAttr = "catalogID = " + catalogID + "";

                IList<Article> _articleList = _articleService.PaginatedArticle(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

                string uri = "List.do?catalogID=" + catalogID + "&pageIndex={0}";
                string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

                PropertyBag["catalog"] = _catalog;
                PropertyBag["articleList"] = _articleList;
                PropertyBag["ltlShowPager"] = ltlShowPager;
            }
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void ScheNoticeList(string userName)
        {
            IDictionary _hash = new Hashtable();
            _hash.Add("UserName", userName);

            PropertyBag["scheNoticeList"] = _scheNoticeService.GetScheNotice(_hash);
        }


        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void Article(int articleID)
        {

            Article _article = _articleService.GetArticleByID(articleID);
            if (_article != null)
            {
                Catalog _catalog = _catalogService.GetCatalogByID(_article.CatalogID);
                PropertyBag["catalog"] = _catalog;
            }

            PropertyBag["article"] = _article;
        }

        /// <summary>
        /// 返回视频列表
        /// </summary>
        /// <param name="stID">班级</param>
        /// <param name="pageIndex"></param>
        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void VideoList(int stID = 0, int pageIndex = 1)
        {
            PropertyBag["catalog"] = -1;

            int _totalNums = 0, _pageSize = 12;
            if (pageIndex < 1) pageIndex = 1;

            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "SchoolTerm.ID", "SchoolTerm.ID IN(SELECT [STID] FROM [JNyulu_db].[dbo].[Video](NOLOCK) WHERE Permit=0)", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

            string _conAttr = "permit = 0";
            if (stID > 0)
            {
                _conAttr += " AND STID = " + stID;
            }

            SchoolTerm _schoolTerm = _schoolTermService.GetBaseSchoolTermByID(stID);
            if (_schoolTerm == null)
            {
                _schoolTerm = new SchoolTerm();
                _schoolTerm.Name = "雨露微课堂";
            }
            PropertyBag["schoolTerm"] = _schoolTerm;

            IList<Video> _videoList = _videoService.PaginatedVideo(pageIndex, _pageSize, "SortNum", _conAttr, ref _totalNums);

            string uri = "VideoList.do?stID=" + stID + "&pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["videoList"] = _videoList;
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        /// <summary>
        /// 视频信息
        /// </summary>
        /// <param name="videoID"></param>
        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void Video(int videoID)
        {
            int _totalNums = 0;
            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "SchoolTerm.ID", "SchoolTerm.ID IN(SELECT [STID] FROM [JNyulu_db].[dbo].[Video](NOLOCK) WHERE Permit=0)", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

            Video _video = _videoService.GetVideoByID(videoID);

            if (_video != null)
            {
                SchoolTerm _schoolTerm = _schoolTermService.GetBaseSchoolTermByID(_video.STID);
                if (_schoolTerm == null)
                {
                    _schoolTerm = new SchoolTerm();
                    _schoolTerm.Name = "雨露微课堂";
                }
                PropertyBag["schoolTerm"] = _schoolTerm;
            }

            PropertyBag["video"] = _video;
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void Message()
        {

        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void MessageList(int pageIndex)
        {
            int _totalNums = 0;
            int _pageSize = 20;
            if (pageIndex < 1) pageIndex = 1;

            string _conAttr = "1 = 1";

            IList<Message> _messageList = _messageService.PaginatedMessage(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            string uri = "javascript:jsPaginated({0});";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["messageList"] = _messageList;
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void CreateMessage()
        {

        }

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void PostCreateMessage(string userName, string subject, string issue)
        {
            int _code = 0;
            string _message = "提交消息失败";

            if (!string.IsNullOrEmpty(subject))
            {
                MyMessage _myMessage = new MyMessage();
                _myMessage.UserName = userName;
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

        [SkipFilterAttribute(typeof(AuthenticationFilter))]
        public void Mobile()
        {

        }
    }
}
