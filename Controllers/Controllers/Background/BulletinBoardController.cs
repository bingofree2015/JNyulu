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
    /* 网站咨询  */
    [ControllerDetails(Area = "Background")]
    public class BulletinBoardController : DefaultController
    {
        protected readonly BulletinBoardService _bulletinBoardService = BulletinBoardService.GetInstance();
        protected readonly SchoolTermService _schoolTermService = SchoolTermService.GetInstance();

        public void List(int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            int _totalNums = 0, _pageSize = 12;
            if (pageIndex < 1) pageIndex = 1;

            string _conAttr = "1 = 1";

            IList<BulletinBoard> _bulletinBoardList = _bulletinBoardService.PaginatedBulletinBoard(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["bulletinBoardList"] = _bulletinBoardList;

            string uri = "BulletinBoardList.do?pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void Create(int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;
        }

        public void PostCreate(int receiver, string subject, string msgText)
        {
            int _code = 0;
            string _message = "发布公告失败";
            Employee _employee = Session["logonUser"] as Employee;

            if (!string.IsNullOrEmpty(subject))
            {
                BulletinBoard _bulletinBoard = new BulletinBoard();

                _bulletinBoard.Sender = _employee.ID;
                _bulletinBoard.Receiver = receiver;
                _bulletinBoard.Subject = subject;
                _bulletinBoard.MsgText = msgText;

                _code = _bulletinBoardService.InsertBulletinBoard(_bulletinBoard);

                if (_code > 0)
                {
                    _message = "发布公告成功";
                }
                else
                {
                    _message = "对不起，发布公告失败，可能有重名！";
                }
            }
            else
            {
                _message = "公告标题不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int bulletinBoardID,int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            IList<SchoolTerm> _schoolTermList = _schoolTermService.PaginatedSchoolTerm(1, 120, "ID", "1 = 1", ref _totalNums);
            PropertyBag["schoolTermList"] = _schoolTermList;

            BulletinBoard _bulletinBoard = _bulletinBoardService.GetBulletinBoardByID(bulletinBoardID);
            PropertyBag["bulletinBoard"] = _bulletinBoard;
        }

        public void PostEdit(int bulletinBoardID, string subject, int receiver, string msgText)
        {
            int _code = 0;
            string _message = "修改公告失败";
            Employee _employee = Session["logonUser"] as Employee;

            if (!string.IsNullOrEmpty(subject))
            {
                BulletinBoard _bulletinBoard = new BulletinBoard();

                _bulletinBoard.ID = bulletinBoardID;
                _bulletinBoard.Sender = _employee.ID;
                _bulletinBoard.Subject = subject;
                _bulletinBoard.Receiver = receiver;
                _bulletinBoard.MsgText = msgText;

                _code = _bulletinBoardService.UpdateBulletinBoard(_bulletinBoard);

                if (_code > 0)
                {
                    _message = "修改公告成功";
                }
                else
                {
                    _message = "对不起，修改失败！";
                }
            }
            else
            {
                _message = "公告失败不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int bulletinBoardID)
        {
            int _code = 0;
            string _message = "删除公告信息失败";

            _code = _bulletinBoardService.DeleteBulletinBoardByID(bulletinBoardID);

            if (_code > 0)
            {
                _message = "删除公告信息成功";
            }
            else
            {
                _message = "对不起，删除失败！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
