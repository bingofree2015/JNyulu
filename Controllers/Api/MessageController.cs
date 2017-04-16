using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MonoRail.Framework;

using Newtonsoft.Json;

using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web.Api.Controllers
{
    [ControllerDetails(Area = "Api")]
    public class MessageController : ApiController
    {
        protected readonly MessageService _messageService = MessageService.GetInstance();

        /// <summary>
        /// 返回留言列表
        /// </summary>
        /// <param name="pageIndex"></param>
        public void List(string userName, int pageIndex)
        {
            int _totalNums = 0, _pageSize = 12;
            pageIndex = pageIndex < 1 ? 1 : pageIndex;

            string _conAttr = "UserName = '" + userName + "'";

            IList<Message> _messageList = _messageService.PaginatedMessage(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            _result.Code = 0;
            //_result.Data = _messageList;
            _result.Data = new
            {
                List = _messageList,
                CurrentPage = pageIndex,
                TotalPage = (_totalNums % _pageSize) > 0 ? (_totalNums / _pageSize) + 1 : _totalNums / _pageSize
            };
            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);
            CancelLayout();
        }

        /// <summary>
        /// 提交留言
        /// </summary>
        /// <param name="articleID"></param>
        public void PostMessage(string userName, string subject, string issue)
        {
            if (!string.IsNullOrEmpty(subject))
            {
                Message _message = new Message();
                _message.UserName = userName;
                _message.Subject = subject;
                _message.Issue = issue;

                _result.Code = _messageService.InsertMessage(_message);
                _result.Data = "您的问题已经成功提交，请等候回复...";
            }
            else
            {
                _result.Data = "对不起，标题不能为空！";
            }

            string _jsonString = JsonConvert.SerializeObject(_result, Formatting.Indented);

            RenderText(_jsonString);

            CancelLayout();
        }

    }
}