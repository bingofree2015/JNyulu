using System;
using System.IO;
using System.Web;
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
    /* 网站咨询  */
    [ControllerDetails(Area = "Background")]
    public class MessageController : DefaultController
    {
        protected readonly MessageService _messageService = MessageService.GetInstance();

        public void List(int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            int _pageSize = 12;
            if (pageIndex < 1) pageIndex = 1;

            string _conAttr = "1 = 1";

            IList<Message> _messageList = _messageService.PaginatedMessage(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["messageList"] = _messageList;

            string uri = "messageList.do?pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void Edit(int messageID, int pageIndex)
        {
            PropertyBag["params"] = new
            {
                PageIndex = pageIndex
            };

            Message _message = _messageService.GetMessageByID(messageID);
            PropertyBag["message"] = _message;
        }

        public void PostEdit(int messageID, string subject, string issue, string reply)
        {
            int _code = 0;
            string _message = "回复问题失败";
            if (!string.IsNullOrEmpty(subject))
            {
                MyMessage _myMessage = new MyMessage();
                _myMessage.ID = messageID;
                _myMessage.Subject = subject;
                _myMessage.Issue = issue;
                _myMessage.Reply = reply;

                _code = _messageService.UpdateMessage(_myMessage);

                if (_code > 0)
                {
                    _message = "回复问题成功";
                }
                else
                {
                    _message = "对不起，操作失败！";
                }
            }
            else
            {
                _message = "标题不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int messageID)
        {
            int _code = 0;
            string _message = "删除公告信息失败";

            _code = _messageService.DeleteMessageByID(messageID);

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
